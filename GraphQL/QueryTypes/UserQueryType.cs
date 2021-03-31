using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using Magicord.Core.Security;
using Magicord.Models;
using Magicord.Modules.Users;
using Microsoft.EntityFrameworkCore;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    [DiscordOnly]
    [UseProjection]
    public IQueryable<User> GetUsers([Service] IUserService userService)
    {
      return userService.GetUsers();
    }

    [DiscordOnly]
    [UseProjection]
    public IQueryable<User> GetUser([Service] IUserService userService, long id)
    {
      return userService.GetUserById(id);
    }
  }
}