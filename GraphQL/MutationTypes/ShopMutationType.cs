using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Shop;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public Card BuylistCard([Service] IShopService shopService, BuylistCardInputDto input)
    {
      return shopService.BuylistCard(input);
    }
  }
}