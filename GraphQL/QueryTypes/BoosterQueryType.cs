using System;
using System.Collections.Generic;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Booster;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    public List<Card> GetBoosterBySet([Service] IBoosterService boosterService, string setCode)
    {
      return boosterService.GenerateBooster(setCode);
    }
  }
}