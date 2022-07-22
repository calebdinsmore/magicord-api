using System.Threading.Tasks;
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

    public DraftDeckImportResultDto ImportDraftDeck([Service] IUserService userService, DraftDeckImportInputDto input)
    {
      return userService.ImportDraftDeck(input);
    }

    public Task<StockTransactionResultDto> BuyShares([Service] IUserService userService, StockTransactionInputDto input)
    {
      return userService.BuySharesAsync(input);
    }

    public Task<StockTransactionResultDto> SellShares([Service] IUserService userService, StockTransactionInputDto input)
    {
      return userService.SellSharesAsync(input);
    }

    public Task<StockTransactionResultDto> ShortShares([Service] IUserService userService, StockTransactionInputDto input)
    {
      return userService.ShortSharesAsync(input);
    }

    public Task<StockTransactionResultDto> ReduceShort([Service] IUserService userService, ShortAdjustmentInputDto input)
    {
      return userService.ReduceShortPositionAsync(input);
    }

    public StockTransactionResultDto BolsterShort([Service] IUserService userService, ShortAdjustmentInputDto input)
    {
      return userService.AddToShortCashReserve(input);
    }

    public Task<StockTransactionResultDto> CloseShort([Service] IUserService userService, ShortAdjustmentInputDto input)
    {
      return userService.CloseShortPositionAsync(input);
    }

    public async Task<bool> SyncPortfolio([Service] IUserService userService, long userId)
    {
      await userService.SyncPortfolio(userId);
      return true;
    }
  }
}