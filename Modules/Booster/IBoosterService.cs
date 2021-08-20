using System.Collections.Generic;
using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Booster
{
  public interface IBoosterService
  {
    List<BoosterCardDto> BuyRandomBooster(long userId);
    List<BoosterPackDto> BuyMultipleBoosters(long userId, string setCode, int count);
    List<BoosterCardDto> BuySingleBooster(long userId, string setCode);
    void AddBoosterCardsToUser(List<BoosterCardDto> boosterCards, User user);
    List<BoosterCardDto> GenerateBooster(string setCode);
    List<BoosterPackDto> GenerateMultipleBoosters(string setCode, int count);
    List<BoosterStatsDto> GetBoosterStatsForAllSetListings();
    BoosterStatsDto GetBoosterStats(string setCode);
    StoreBoosterListing AddBoosterListing(string setCode, decimal retailPrice);
    IQueryable<StoreBoosterListing> GetStoreBoosterListings();
  }
}