using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Magicord.Models;
using Magicord.Modules.Booster;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    public List<BoosterCardDto> GetBoosterBySet([Service] IBoosterService boosterService, string setCode)
    {
      return boosterService.GenerateBooster(setCode);
    }

    public BoosterStatsDto GetBoosterStats([Service] IBoosterService boosterService, string setCode)
    {
      return boosterService.GetBoosterStats(setCode);
    }

    [UseProjection]
    public IQueryable<StoreBoosterListing> GetStoreBoosterListings([Service] IBoosterService boosterService)
    {
      return boosterService.GetStoreBoosterListings();
    }
  }
}