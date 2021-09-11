using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    Task<StockTransactionResultDto> BuySharesAsync(StockTransactionInputDto input);

    Task<StockTransactionResultDto> SellSharesAsync(StockTransactionInputDto input);

    Task<StockTransactionResultDto> ShortSharesAsync(StockTransactionInputDto input);

    StockTransactionResultDto AddToShortCashReserve(ShortAdjustmentInputDto input);

    Task<StockTransactionResultDto> ReduceShortPositionAsync(ShortAdjustmentInputDto input);

    Task<StockTransactionResultDto> CloseShortPositionAsync(ShortAdjustmentInputDto input);
  }
}