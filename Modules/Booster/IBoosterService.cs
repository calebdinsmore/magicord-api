using System.Collections.Generic;
using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Booster
{
  public interface IBoosterService
  {
    List<BoosterCardDto> BuyRandomBooster(long userId);
    List<BoosterPackDto> BuyMultipleBoosters(long userId, string setCode, int count);
    List<BoosterCardDto> BuyBooster(long userId, string setCode);
    List<BoosterCardDto> GenerateBooster(string setCode);
    List<BoosterStatsDto> GetBoosterStatsForAllSetListings();
    BoosterStatsDto GetBoosterStats(string setCode);
    StoreBoosterListing AddBoosterListing(string setCode, decimal retailPrice);
    IQueryable<StoreBoosterListing> GetStoreBoosterListings();
  }
}