using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using Magicord.Models;
using Magicord.Modules.Users;
using Microsoft.EntityFrameworkCore;

namespace Magicord.GraphQL.QueryTypes
{
  public class UserQueryType
  {
    [UseProjection]
    public IQueryable<User> GetUsers([Service] IUserService userService)
    {
      return userService.GetUsers();
    }

    [UseProjection]
    public IQueryable<User> GetUser([Service] IUserService userService, long id)
    {
      return userService.GetUserById(id);
    }
  }
}