using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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