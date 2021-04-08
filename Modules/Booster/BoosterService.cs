using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Magicord.Core.Exceptions;
using Magicord.Models;
using Magicord.Modules.Core.Extension;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Booster
{
  public class BoosterService : IBoosterService
  {
    private readonly MagicordContext _dataContext;
    private readonly IMapper _mapper;
    private readonly Random _random;

    public BoosterService(MagicordContext dataContext, IMapper mapper, Random random)
    {
      _dataContext = dataContext;
      _mapper = mapper;
      _random = random;
    }

    public List<Card> GenerateBooster(string setCode)
    {
      var boosterCardList = new List<Card>();
      var set = _dataContext.Sets.FirstOrDefault(x => x.Code == setCode);
      if (set != null && set.Booster != null)
      {
        var boosterJson = set.Booster.ParseJson<BoosterJson>();
        if (boosterJson.Default != null)
        {
          var boosterContentConfig = ChooseBoosterContent(boosterJson.Default);
          foreach (var sheetName in boosterContentConfig.Contents.Keys)
          {
            AddRandomCardsFromSheet(boosterCardList, sheetName, boosterJson.Default, boosterContentConfig);
          }
        }
      }

      return boosterCardList;
    }

    private BoosterContentConfig ChooseBoosterContent(BoosterConfig boosterConfig)
    {
      var randomValue = _random.Next(boosterConfig.BoostersTotalWeight);
      foreach (var booster in boosterConfig.Boosters)
      {
        randomValue -= booster.Weight;
        if (randomValue < 0)
        {
          return booster;
        }
      }

      // It should never come here
      throw new InvalidMagicordOperationException($"Unable to select booster for set (bad weight data?)");
    }

    private void AddRandomCardsFromSheet(List<Card> cardList, string sheetName, BoosterConfig boosterConfig, BoosterContentConfig boosterContentConfig)
    {
      var sheet = boosterConfig.Sheets.GetValueOrDefault(sheetName);

      var cardUuids = new List<string>();
      for (var i = 0; i < boosterContentConfig.Contents.GetValueOrDefault(sheetName); i++)
      {
        var randomValue = _random.Next(sheet.TotalWeight);
        foreach (var cardUuid in sheet.Cards.Keys)
        {
          randomValue -= sheet.Cards.GetValueOrDefault(cardUuid);
          if (randomValue < 0)
          {
            cardUuids.Add(cardUuid);
            // cardList.Add(_dataContext.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Uuid == cardUuid));
            break;
          }
        }
      }
      cardList.AddRange(_dataContext.Cards.Include(x => x.CardPrice).Where(x => cardUuids.Any(y => y == x.Uuid)));
    }

    public List<Card> BuyBooster(long userId, string setCode)
    {
      var boosterListing = _dataContext.StoreBoosterListings.FirstOrDefault(x => x.SetCode == setCode);
      var user = _dataContext.Users.Include(x => x.UserCards).FirstOrDefault(x => x.Id == userId);

      if (boosterListing == null)
      {
        throw new ValidationFailureException($"Could not find a store booster listing matching set {setCode}");
      }

      if (user == null)
      {
        throw new ValidationFailureException($"Could not find a user with ID {userId}");
      }

      if (user.Balance - boosterListing.RetailPrice < 0)
      {
        throw new ValidationFailureException($"Insufficient funds to purchase booster.");
      }

      user.Balance -= boosterListing.RetailPrice;
      var boosterCards = GenerateBooster(setCode);

      foreach (var card in boosterCards)
      {
        var userCard = user.UserCards.FirstOrDefault(x => x.CardUuid == card.Uuid);
        if (userCard != null)
        {
          userCard.Quantity += 1;
        }
        else
        {
          user.UserCards.Add(new UserCard
          {
            UserId = user.Id,
            CardUuid = card.Uuid,
            Quantity = 1
          });
        }
      }

      _dataContext.SaveChanges();
      return boosterCards;
    }

    public BoosterStatsDto GetBoosterStats(string setCode)
    {
      var numberOfRuns = 300;
      var boosterStats = new BoosterStatsDto
      {
        AverageBuylistPrice = 0,
        AverageRetailPrice = 0
      };
      for (var i = 0; i < numberOfRuns; i++)
      {
        var booster = GenerateBooster(setCode);
        decimal boosterTotalBuylist = 0;
        decimal boosterTotalRetail = 0;
        foreach (var card in booster)
        {
          if (card.HasNonFoil)
          {
            boosterTotalRetail += card.CardPrice.CurrentRetailNonFoil;
            boosterTotalBuylist += card.CardPrice.CurrentBuylistNonFoil;
          }
          else
          {
            boosterTotalRetail += card.CardPrice.CurrentRetailFoil;
            boosterTotalBuylist += card.CardPrice.CurrentBuylistFoil;
          }
        }
        boosterStats.AverageBuylistPrice += boosterTotalBuylist;
        boosterStats.AverageRetailPrice += boosterTotalRetail;
      }
      boosterStats.AverageBuylistPrice = boosterStats.AverageBuylistPrice / numberOfRuns;
      boosterStats.AverageRetailPrice = boosterStats.AverageRetailPrice / numberOfRuns;
      return boosterStats;
    }
  }
}