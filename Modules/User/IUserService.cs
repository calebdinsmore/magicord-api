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

    UserStatsDto GetUserStats(long id);

    List<UserCollectionValueChangeDto> GetChangeInUserCollectionValues();
  }
}