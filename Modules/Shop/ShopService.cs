using AutoMapper;
using HotChocolate.Execution;
using Magicord.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

    public BuylistBulkResultDto BuylistAll(long userId)
    {
      var user = _dataContext.Users.Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice).Where(x => x.Id == userId).FirstOrDefault();
      if (user == null)
      {
        throw new QueryException("User not found. Have you done `mc start`?");
      }
      var totalPayout = 0M;
      var numCardsSold = 0;
      foreach (var userCard in user.UserCards)
      {
        var foilPayout = userCard.AmountFoil * userCard.Card.CardPrice.CurrentBuylistFoil;
        var nonFoilPayout = userCard.AmountNonFoil * userCard.Card.CardPrice.CurrentBuylistNonFoil;
        totalPayout += foilPayout + nonFoilPayout;
        numCardsSold += userCard.AmountFoil + userCard.AmountNonFoil;
      }
      user.Balance += totalPayout;
      _dataContext.RemoveRange(user.UserCards);
      _dataContext.SaveChanges();
      return new BuylistBulkResultDto
      {
        NumCardsSold = numCardsSold,
        TotalPayout = totalPayout
      };
    }

    public BuylistCardResultDto BuylistCard(BuylistCardInputDto input)
    {
      var user = _dataContext.Users.Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice).FirstOrDefault(x => x.Id == input.UserId);
      if (user == null)
      {
        throw new QueryException("Can't find user. Have you done `mc start`?");
      }

      IEnumerable<UserCard> userCards;
      if (!string.IsNullOrWhiteSpace(input.CardName))
      {
        userCards = user.UserCards.Where(x => x.Card.Name.ToLower() == input.CardName.ToLower());
      }
      else if (input.CardId > 0)
      {
        userCards = user.UserCards.Where(x => x.Card.Id == input.CardId);
      }
      else
      {
        throw new QueryException("You must either supply a card name or a card ID");
      }

      if (!userCards.Any())
      {
        throw new QueryException($"Could not find a matching card belonging to you.");
      }

      if (userCards.Count() > 1)
      {
        return new BuylistCardResultDto
        {
          CardSuggestions = userCards.Take(10).ToList()
        };
      }

      var userCard = userCards.First();

      if (input.Amount > userCard.AmountNonFoil || input.AmountFoil > userCard.AmountFoil)
      {
        throw new QueryException("Number supplied exceeds number of cards owned.");
      }

      userCard.AmountFoil -= input.AmountFoil;
      userCard.AmountNonFoil -= input.Amount;

      if (userCard.AmountFoil + userCard.AmountNonFoil == 0)
      {
        _dataContext.Remove(userCard);
      }

      user.Balance += input.Amount * userCard.Card.CardPrice.CurrentBuylistNonFoil;
      user.Balance += input.AmountFoil * userCard.Card.CardPrice.CurrentBuylistFoil;

      _dataContext.SaveChanges();
      return new BuylistCardResultDto
      {
        Card = userCard.Card
      };
    }

    public BuylistBulkResultDto BuylistExtra(long userId, int amount = 4)
    {
      var exceptionCards = new List<string>(new string[]
      {
        "Persistent Petitioners",
        "Relentless Rats",
        "Dragon's Approach",
        "Rat Colony",
        "Shadowborn Apostle",
        "Seven Dwarves"
      });

      var user = _dataContext.Users.Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice).FirstOrDefault(x => x.Id == userId);
      if (user == null)
      {
        throw new QueryException("Can't find user. Have you done `mc start`?");
      }

      var extraCards = user.UserCards
        .Where(x =>
          !x.IsLocked
          && (x.Card.Supertypes == null || !x.Card.Supertypes.StartsWith("Basic"))
          && !exceptionCards.Contains(x.Card.Name)
          && (x.AmountFoil > amount || x.AmountNonFoil > amount));

      var totalPayout = 0M;
      var numCardsSold = 0;
      foreach (var userCard in extraCards)
      {
        if (userCard.AmountNonFoil > amount)
        {
          var amountToSell = userCard.AmountNonFoil - amount;
          var payout = amountToSell * userCard.Card.CardPrice.CurrentBuylistNonFoil;
          user.Balance += payout;
          totalPayout += payout;
          numCardsSold += amountToSell;
          userCard.AmountNonFoil = amount;
        }
        if (userCard.AmountFoil > amount)
        {
          var amountToSell = userCard.AmountFoil - amount;
          var payout = amountToSell * userCard.Card.CardPrice.CurrentBuylistFoil;
          user.Balance += payout;
          totalPayout += payout;
          numCardsSold += amountToSell;
          userCard.AmountFoil = amount;
        }
      }
      _dataContext.SaveChanges();
      return new BuylistBulkResultDto
      {
        NumCardsSold = numCardsSold,
        TotalPayout = totalPayout
      };
    }

    public BuylistBulkResultDto BuylistFoils(long userId)
    {
      var user = _dataContext.Users.Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice).FirstOrDefault(x => x.Id == userId);
      if (user == null)
      {
        throw new QueryException("Can't find user. Have you done `mc start`?");
      }

      var foils = user.UserCards.Where(card => card.AmountFoil > 0);
      var result = new BuylistBulkResultDto();
      foreach (var userCard in foils)
      {
        var payout = userCard.Card.CardPrice.CurrentBuylistFoil * userCard.AmountFoil;
        result.NumCardsSold += userCard.AmountFoil;
        result.TotalPayout += payout;

        userCard.AmountFoil = 0;
      }

      user.Balance += result.TotalPayout;
      _dataContext.SaveChanges();
      return result;
    }

    public UserCard BuylistLock(long userId, long cardId)
    {
      var user = _dataContext.Users
        .Include(u => u.UserCards).ThenInclude(uc => uc.Card)
        .FirstOrDefault(u => u.Id == userId);
      if (user == null)
      {
        throw new QueryException("Could not find user. Have you done `mc start`?");
      }
      var userCard = user.UserCards.FirstOrDefault(uc => uc.Card.Id == cardId);
      if (userCard == null)
      {
        throw new QueryException("Could not a find a card belonging to you matching that id.");
      }
      userCard.IsLocked = !userCard.IsLocked;
      _dataContext.SaveChanges();
      return userCard;
    }
  }
}