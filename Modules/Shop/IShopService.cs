using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Shop
{
  public interface IShopService
  {
    BuylistCardResultDto BuylistCard(BuylistCardInputDto input);

    BuylistBulkResultDto BuylistAll(long userId);

    BuylistBulkResultDto BuylistExtra(long userId);
  }
}