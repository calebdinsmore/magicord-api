using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Magicord.Models;
using Magicord.Modules.Booster;

namespace Magicord.GraphQL.QueryTypes
{
  public class Test
  {
    public decimal Value { get; set; }
    public List<BoosterCardDto> Cards { get; set; }
  }
  public partial class Query
  {
    public List<BoosterCardDto> GetBoosterBySet([Service] IBoosterService boosterService, string setCode)
    {
      return boosterService.GenerateBooster(setCode);
    }

    public Test GetBoosterValueBySet([Service] IBoosterService boosterService, string setCode)
    {
      var booster = boosterService.GenerateBooster(setCode);
      return new Test
      {
        Value = booster.Sum(x => x.Card.CardPrice.CurrentBuylistNonFoil),
        Cards = booster
      };
    }

    public BoosterStatsDto GetBoosterStats([Service] IBoosterService boosterService, string setCode)
    {
      return boosterService.GetBoosterStats(setCode);
    }

    public List<BoosterStatsDto> GetBoosterStatsForAllListings([Service] IBoosterService boosterService)
    {
      return boosterService.GetBoosterStatsForAllSetListings();
    }

    [UseProjection]
    public IQueryable<StoreBoosterListing> GetStoreBoosterListings([Service] IBoosterService boosterService)
    {
      return boosterService.GetStoreBoosterListings();
    }
  }
}