using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicord.Modules.Users.Dto;
using Magicord.Modules.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Magicord.Core.Security;

namespace Magicord.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserManager _userManager;
    public UsersController(IUserManager userManager)
    {
      _userManager = userManager;
    }

    // GET api/users
    [HttpGet("")]
    [DiscordOnly]
    public IEnumerable<UserDto> GetUsers()
    {
      return _userManager.GetAll();
    }

    // GET api/users/5
    [HttpGet("{id}")]
    public ActionResult<UserDto> GetUserById(int id)
    {
      return null;
    }

    // POST api/users
    [HttpPost("")]
    public long Upsert(UserDto value)
    {
      return _userManager.Upsert(value);
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    public void DeleteUserById(int id)
    {
    }
  }
}