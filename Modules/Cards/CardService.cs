using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.AdminProcess;
using Magicord.Modules.Integration.TcgPlayer;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Cards
{
  public class CardService : ICardService
  {
    private readonly MagicordContext _dataContext;
    private readonly IMapper _mapper;
    private readonly IAdminProcessService _adminProcessService;
    private readonly ITcgPlayerService _tcgPlayerService;

    public CardService(MagicordContext dataContext, IMapper mapper, IAdminProcessService adminProcessService, ITcgPlayerService tcgPlayerService)
    {
      _dataContext = dataContext;
      _mapper = mapper;
      _adminProcessService = adminProcessService;
      _tcgPlayerService = tcgPlayerService;
    }

    public async Task<IQueryable<Card>> GetCardsMatchingQuery(CardSearchInputDto input)
    {
      if (string.IsNullOrEmpty(input.Name))
      {
        throw new QueryException("'Name' argument required.");
      }

      var exactMatch = _dataContext.Cards
        .Include(x => x.CardPrice)
        .ThenInclude(x => x.CardPriceHistories.OrderByDescending(y => y.DateRecorded).Take(1))
        .Where(x => x.Name.ToLower() == input.Name.ToLower());
      if (!string.IsNullOrEmpty(input.SetCode))
      {
        exactMatch = exactMatch.Where(x => x.SetCode.ToLower() == input.SetCode.ToLower());
      }

      if (exactMatch.Any())
      {
        if (input.UpdatePrices && exactMatch.Count() < 4)
        {
          var materializedCards = exactMatch.ToList();
          foreach (var card in materializedCards)
          {
            await CheckForPriceChangeAsync(card);
          }
        }
        return exactMatch;
      }

      if (input.Name.Length < 3)
      {
        throw new QueryException("Card search query too short.");
      }

      // if no exact match, try a broader match
      var matchingCards = _dataContext.Cards
        .Where(x => EF.Functions.ILike(x.Name, $"{input.Name}%"));

      if (!string.IsNullOrEmpty(input.SetCode))
      {
        matchingCards = matchingCards.Where(x => x.SetCode.ToLower() == input.SetCode.ToLower());
      }

      if (matchingCards.Select(x => x.Name).Distinct().Count() > 1)
      {
        throw new QueryException($"Multiple cards matched {input.Name}. Try being more specific?");
      }

      if (input.UpdatePrices && matchingCards.Count() < 5)
      {
        var materializedCards = matchingCards.ToList();
        foreach (var card in materializedCards)
        {
          await CheckForPriceChangeAsync(card);
        }
      }

      return matchingCards;
    }

    public async Task CheckForPriceChangeAsync(Card card)
    {
      try
      {
        if (card.TcgplayerProductId == null)
        {
          return;
        }
        var pricePoints = await _tcgPlayerService.GetCardPricePoints(card.TcgplayerProductId);
        var foilPrice = pricePoints.FirstOrDefault(x => x.PrintingType == PrintingTypeEnum.Foil)?.MarketPrice ?? 0;
        var nonFoilPrice = pricePoints.FirstOrDefault(x => x.PrintingType == PrintingTypeEnum.Normal)?.MarketPrice ?? 0;

        if (foilPrice != card.CardPrice.CurrentRetailFoil || nonFoilPrice != card.CardPrice.CurrentRetailNonFoil)
        {
          _adminProcessService.ArchiveCardPrice(card.CardPrice);
          card.CardPrice.CurrentRetailFoil = foilPrice;
          card.CardPrice.CurrentRetailNonFoil = nonFoilPrice;
          _dataContext.SaveChanges();
        }
      }
      catch (System.Exception e)
      {
        Console.WriteLine($"Encountered an error fetching latest prices for {card.Id}.\n{e.ToString()}");
      }
    }
  }
}