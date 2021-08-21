using System.Collections.Generic;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Booster;
using Magicord.Modules.Users;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public List<BoosterCardDto> BuyBooster([Service] IBoosterService boosterService, long userId, string setCode)
    {
      return boosterService.BuySingleBooster(userId, setCode);
    }

    public List<BoosterCardDto> BuyRandomBooster([Service] IBoosterService boosterService, long userId)
    {
      return boosterService.BuyRandomBooster(userId);
    }

    public List<BoosterPackDto> BuyMultipleBoosters([Service] IBoosterService boosterService, long userId, string setCode, int count)
    {
      return boosterService.BuyMultipleBoosters(userId, setCode, count);
    }

    public StoreBoosterListing AddBoosterListing([Service] IBoosterService boosterService, string setCode, decimal retailPrice)
    {
      return boosterService.AddBoosterListing(setCode, retailPrice);
    }

    public List<BoosterPackDto> GiveBoostersToUser([Service] IBoosterService boosterService, long userId, string setCode, int count)
    {
      return boosterService.GiveBoostersToUser(userId, setCode, count);
    }
  }
}