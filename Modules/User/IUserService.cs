using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Users
{
  public interface IUserService
  {
    User CreateUser(CreateUserInputDto dto);

    IQueryable<User> GetUsers();

    IQueryable<User> GetUserById(long id);
  }
}