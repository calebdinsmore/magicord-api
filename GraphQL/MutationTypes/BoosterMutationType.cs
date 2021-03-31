using System.Collections.Generic;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Booster;
using Magicord.Modules.Users;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public List<Card> BuyBooster([Service] IBoosterService boosterService, long userId, string setCode)
    {
      return boosterService.BuyBooster(userId, setCode);
    }
  }
}