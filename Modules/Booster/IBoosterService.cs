using System.Collections.Generic;
using Magicord.Models;

namespace Magicord.Modules.Booster
{
  public interface IBoosterService
  {
    List<Card> BuyBooster(long userId, string setCode);
    List<Card> GenerateBooster(string setCode);
  }
}