using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Magicord.Models;
using Magicord.Modules.Users;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    [Authorize]
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