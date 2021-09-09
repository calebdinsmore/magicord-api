using System.Collections.Generic;
using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Users
{
  public interface IUserService
  {
    User CreateUser(CreateUserInputDto dto);

    User AddToUserBalance(AddToUserBalanceInputDto dto);

    IQueryable<User> GetUsers();

    IQueryable<User> GetUserById(long id);

    IQueryable<UserCard> GetUserCardsById(long id);

    IQueryable<UserShare> GetUserSharesById(long id);

    IQueryable<UserShort> GetUserShortsById(long id);

    UserStatsDto GetUserStats(long id);

    List<UserCollectionValueChangeDto> GetChangeInUserCollectionValues();

    StockTransactionResultDto BuyShares(StockTransactionInputDto input);

    StockTransactionResultDto SellShares(StockTransactionInputDto input);

    StockTransactionResultDto ShortShares(StockTransactionInputDto input);

    StockTransactionResultDto AddToShortCashReserve(ShortAdjustmentInputDto input);

    StockTransactionResultDto ReduceShortPosition(ShortAdjustmentInputDto input);

    StockTransactionResultDto CloseShortPosition(ShortAdjustmentInputDto input);
  }
}