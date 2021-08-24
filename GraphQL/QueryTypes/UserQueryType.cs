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

    [UseProjection]
    public IQueryable<UserCard> GetUserCards([Service] IUserService userService, long id)
    {
      return userService.GetUserCardsById(id);
    }

    public UserStatsDto GetUserStats([Service] IUserService userService, long id)
    {
      return userService.GetUserStats(id);
    }

    public List<UserCollectionValueChangeDto> GetChangeInUserCollectionValues([Service] IUserService service)
    {
      return service.GetChangeInUserCollectionValues();
    }
  }
}