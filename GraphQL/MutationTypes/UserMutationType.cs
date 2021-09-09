using AutoMapper;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Users;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public User CreateUser([Service] IUserService userService, CreateUserInputDto input)
    {
      return userService.CreateUser(input);
    }

    public User AddToUserBalance([Service] IUserService userService, AddToUserBalanceInputDto input)
    {
      return userService.AddToUserBalance(input);
    }

    public StockTransactionResultDto BuyShares([Service] IUserService userService, StockTransactionInputDto input)
    {
      return userService.BuyShares(input);
    }

    public StockTransactionResultDto SellShares([Service] IUserService userService, StockTransactionInputDto input)
    {
      return userService.SellShares(input);
    }

    public StockTransactionResultDto ShortShares([Service] IUserService userService, StockTransactionInputDto input)
    {
      return userService.ShortShares(input);
    }

    public StockTransactionResultDto ReduceShort([Service] IUserService userService, ShortAdjustmentInputDto input)
    {
      return userService.ReduceShortPosition(input);
    }

    public StockTransactionResultDto BolsterShort([Service] IUserService userService, ShortAdjustmentInputDto input)
    {
      return userService.AddToShortCashReserve(input);
    }

    public StockTransactionResultDto CloseShort([Service] IUserService userService, ShortAdjustmentInputDto input)
    {
      return userService.CloseShortPosition(input);
    }
  }
}