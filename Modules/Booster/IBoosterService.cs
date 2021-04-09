using System.Collections.Generic;
using Magicord.Models;

namespace Magicord.Modules.Booster
{
  public interface IBoosterService
  {
    List<BoosterCardDto> BuyBooster(long userId, string setCode);
    List<BoosterCardDto> GenerateBooster(string setCode);
    BoosterStatsDto GetBoosterStats(string setCode);
    StoreBoosterListing AddBoosterListing(string setCode, decimal retailPrice);
  }
}