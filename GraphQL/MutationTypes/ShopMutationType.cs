using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Shop;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public BuylistCardResultDto BuylistCard([Service] IShopService shopService, BuylistCardInputDto input)
    {
      return shopService.BuylistCard(input);
    }

    public BuylistBulkResultDto BuylistAll([Service] IShopService shopService, long userId)
    {
      return shopService.BuylistAll(userId);
    }

    public BuylistBulkResultDto BuylistExtra([Service] IShopService shopService, long userId, int amount)
    {
      return shopService.BuylistExtra(userId, amount);
    }

    public bool BuylistLock([Service] IShopService shopService, long userId, long cardId)
    {
      shopService.BuylistLock(userId, cardId);
      return true;
    }
  }
}