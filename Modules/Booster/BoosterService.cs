using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotChocolate.Execution;
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

    public IQueryable<StoreBoosterListing> GetStoreBoosterListings()
    {
      return _dataContext.StoreBoosterListings.Where(x => x.IsActive);
    }

    public List<BoosterCardDto> GenerateBooster(string setCode)
    {
      var boosterCardList = new List<BoosterCardDto>();
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

    private void AddRandomCardsFromSheet(List<BoosterCardDto> cardList, string sheetName, BoosterConfig boosterConfig, BoosterContentConfig boosterContentConfig)
    {
      var sheet = boosterConfig.Sheets.GetValueOrDefault(sheetName);

      // var cardUuids = new List<string>();
      var cardUuids = new Dictionary<string, int>();
      for (var i = 0; i < boosterContentConfig.Contents.GetValueOrDefault(sheetName); i++)
      {
        var randomValue = _random.Next(sheet.TotalWeight);
        foreach (var cardUuid in sheet.Cards.Keys)
        {
          randomValue -= sheet.Cards.GetValueOrDefault(cardUuid);
          if (randomValue < 0)
          {
            if (cardUuids.ContainsKey(cardUuid))
            {
              // retry grabbing card
              i--;
              break;
            }

            cardUuids.Add(cardUuid, 1);
            break;
          }
        }
      }
      var cardUuidList = cardUuids.Keys.ToList();
      var boosterCards = _dataContext.Cards.Include(x => x.CardPrice).Where(x => cardUuidList.Any(y => y == x.Uuid))
        .Select(x => new BoosterCardDto
        {
          Card = x,
          Foil = sheet.Foil
        });
      cardList.AddRange(boosterCards);
    }

    public List<BoosterCardDto> BuyRandomBooster(long userId)
    {
      var user = _dataContext.Users.FirstOrDefault(x => x.Id == userId);
      if (user == null)
      {
        throw new QueryException($"User does not exist. Try `mc start`.");
      }

      var boosterListing = _dataContext.StoreBoosterListings
      .Where(x => x.RetailPrice <= user.Balance)
      .OrderBy(x => Guid.NewGuid())
      .Take(1)
      .FirstOrDefault();

      if (boosterListing == null)
      {
        throw new QueryException("Unable to get a random booster within your budget. You broke?");
      }

      var boosterCards = BuyBooster(userId, boosterListing.SetCode);
      _dataContext.SaveChanges();
      return boosterCards;
    }

    public List<BoosterPackDto> BuyMultipleBoosters(long userId, string setCode, int count)
    {
      if (count > 10)
      {
        throw new QueryException("You are not allowed to buy more than 10 boosters at a time.");
      }

      var packs = new List<BoosterPackDto>();
      for (var i = 0; i < count; i++)
      {
        try
        {
          packs.Add(new BoosterPackDto
          {
            Cards = BuyBooster(userId, setCode)
          });
        }
        catch (QueryException)
        {
          if (!packs.Any())
          {
            throw;
          }
          return packs;
        }
      }
      _dataContext.SaveChanges();
      return packs;
    }

    public List<BoosterPackDto> GenerateMultipleBoosters(string setCode, int count)
    {
      var packs = new List<BoosterPackDto>();
      for (var i = 0; i < count; i++)
      {
        try
        {
          packs.Add(new BoosterPackDto
          {
            Cards = GenerateBooster(setCode)
          });
        }
        catch (QueryException)
        {
          if (!packs.Any())
          {
            throw;
          }
          return packs;
        }
      }
      return packs;
    }

    public List<BoosterCardDto> BuyBooster(long userId, string setCode)
    {
      var boosterListing = _dataContext.StoreBoosterListings.FirstOrDefault(x => x.SetCode == setCode);

      if (boosterListing == null)
      {
        throw new QueryException($"Could not find a store booster listing matching set {setCode}");
      }
      var boosterCards = GenerateBooster(setCode);
      var user = _dataContext.Users.Include(x => x.UserCards).FirstOrDefault(x => x.Id == userId);

      if (user == null)
      {
        throw new QueryException($"User does not exist. Try `mc start`.");
      }

      if (user.Balance - boosterListing.RetailPrice < 0)
      {
        throw new QueryException($"Insufficient funds to purchase booster.");
      }

      user.Balance -= boosterListing.RetailPrice;

      AddBoosterCardsToUser(boosterCards, user);

      return boosterCards;
    }

    public void AddBoosterCardsToUser(List<BoosterCardDto> boosterCards, User user)
    {
      foreach (var boosterCard in boosterCards)
      {
        var userCard = user.UserCards.FirstOrDefault(x => x.CardUuid == boosterCard.Card.Uuid);
        if (userCard != null)
        {
          if (boosterCard.Foil)
          {
            userCard.AmountFoil++;
          }
          else
          {
            userCard.AmountNonFoil++;
          }
        }
        else
        {
          user.UserCards.Add(new UserCard
          {
            UserId = user.Id,
            CardUuid = boosterCard.Card.Uuid,
            Quantity = 0,
            AmountFoil = boosterCard.Foil ? 1 : 0,
            AmountNonFoil = boosterCard.Foil ? 0 : 1
          });
        }
      }
    }

    public List<BoosterStatsDto> GetBoosterStatsForAllSetListings()
    {
      var statsList = new List<BoosterStatsDto>();
      var storeListings = _dataContext.StoreBoosterListings.Where(x => x.IsActive).ToList();
      foreach (var listing in storeListings)
      {
        statsList.Add(GetBoosterStats(listing.SetCode));
        Console.WriteLine($"Finished {listing.SetCode}");
      }
      return statsList;
    }

    public BoosterStatsDto GetBoosterStats(string setCode)
    {
      var numberOfRuns = 800;
      var boosterStats = new BoosterStatsDto
      {
        SetCode = setCode,
        AverageBuylistPrice = 0,
        AverageRetailPrice = 0,
        BuylistPrices = new List<decimal>(),
        RetailPrices = new List<decimal>()
      };
      for (var i = 0; i < numberOfRuns; i++)
      {
        var boosterCards = GenerateBooster(setCode);
        decimal boosterTotalBuylist = 0;
        decimal boosterTotalRetail = 0;
        boosterStats.BuylistPrices.Add(boosterCards.Sum(x => x.Foil ? x.Card.CardPrice.CurrentBuylistFoil : x.Card.CardPrice.CurrentBuylistNonFoil));
        boosterStats.RetailPrices.Add(boosterCards.Sum(x => x.Foil ? x.Card.CardPrice.CurrentRetailFoil : x.Card.CardPrice.CurrentRetailNonFoil));
        foreach (var boosterCard in boosterCards)
        {
          if (boosterCard.Foil)
          {
            boosterTotalBuylist += boosterCard.Card.CardPrice.CurrentBuylistFoil;
            boosterTotalRetail += boosterCard.Card.CardPrice.CurrentRetailFoil;
          }
          else
          {
            boosterTotalRetail += boosterCard.Card.CardPrice.CurrentRetailNonFoil;
            boosterTotalBuylist += boosterCard.Card.CardPrice.CurrentBuylistNonFoil;
          }
        }
        boosterStats.AverageBuylistPrice += boosterTotalBuylist;
        boosterStats.AverageRetailPrice += boosterTotalRetail;
      }
      boosterStats.AverageBuylistPrice = boosterStats.AverageBuylistPrice / numberOfRuns;
      boosterStats.AverageRetailPrice = boosterStats.AverageRetailPrice / numberOfRuns;
      return boosterStats;
    }

    public StoreBoosterListing AddBoosterListing(string setCode, decimal retailPrice)
    {
      if (_dataContext.Sets.FirstOrDefault(x => x.Code == setCode) == null)
      {
        throw new ValidationFailureException($"Set code {setCode} does not exist.");
      }

      if (retailPrice < 0)
      {
        throw new ValidationFailureException("Retail price cannot be negative.");
      }

      var entity = new StoreBoosterListing
      {
        SetCode = setCode,
        RetailPrice = retailPrice,
        IsActive = true
      };

      _dataContext.Add(entity);
      _dataContext.SaveChanges();
      return entity;
    }
  }
}