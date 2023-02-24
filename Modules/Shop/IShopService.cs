using Magicord.Models;

namespace Magicord.Modules.Shop
{
  public interface IShopService
  {
    BuylistCardResultDto BuylistCard(BuylistCardInputDto input);

    BuylistBulkResultDto BuylistAll(long userId);

    BuylistBulkResultDto BuylistExtra(long userId, int amount = 4);

    BuylistBulkResultDto BuylistFoils(long userId);

    UserCard BuylistLock(long userId, long cardId);
  }
}