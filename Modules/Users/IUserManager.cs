using System.Collections.Generic;
using Magicord.Modules.Users.Dto;

namespace Magicord.Modules.Users
{
  public interface IUserManager
  {
    IEnumerable<UserDto> GetAll();
    UserDto Create(UserDto dto);
    long Upsert(UserDto dto);
  }
}