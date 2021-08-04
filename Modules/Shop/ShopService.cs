using AutoMapper;
using HotChocolate.Execution;
using Magicord.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Magicord.Modules.Shop
{
  public class ShopService : IShopService
  {
    private readonly MagicordContext _dataContext;
    private readonly IMapper _mapper;
    public ShopService(MagicordContext dataContext, IMapper mapper)
    {
      _dataContext = dataContext;
      _mapper = mapper;
    }

    public Card BuylistCard(BuylistCardInputDto input)
    {
      var user = _dataContext.Users.Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice).FirstOrDefault(x => x.Id == input.UserId);
      if (user == null)
      {
        throw new QueryException("Can't find user. Have you done `mc start`?");
      }

      var userCard = user.UserCards.FirstOrDefault(x => x.Card.Name.ToLower() == input.CardName.ToLower() && x.AmountFoil + x.AmountNonFoil > 0);
      if (userCard == null)
      {
        throw new QueryException($"Could not find a card belonging to you matching '{input.CardName}'.");
      }

      if (input.Amount > userCard.AmountNonFoil || input.AmountFoil > userCard.AmountFoil)
      {
        throw new QueryException("Number supplied exceeds number of cards owned.");
      }

      userCard.AmountFoil -= input.AmountFoil;
      userCard.AmountNonFoil -= input.Amount;

      user.Balance += input.Amount * userCard.Card.CardPrice.CurrentBuylistNonFoil;
      user.Balance += input.AmountFoil * userCard.Card.CardPrice.CurrentBuylistFoil;

      _dataContext.SaveChanges();
      return userCard.Card;
    }
  }
}