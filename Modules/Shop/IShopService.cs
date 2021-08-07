using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Shop
{
  public interface IShopService
  {
    Card BuylistCard(BuylistCardInputDto input);

    BuylistAllResultDto BuylistAll(long userId);
  }
}