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
    private readonly IUserService _userService;
    public UsersController(IUserService userManager)
    {
      _userService = userManager;
    }

    // GET api/users
    // [HttpGet("")]
    // [DiscordOnly]
    // public IEnumerable<UserDto> GetUsers()
    // {
    //   return _userManager.GetAll();
    // }

    // GET api/users/5
    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<UserDto> GetUserById(int id)
    {
      return new UserDto();
    }

    // POST api/users
    // [HttpPost("")]
    // public UserDto CreateUser(UserDto value)
    // {
    //   // return _userManager.Create(value);
    // }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    public void DeleteUserById(int id)
    {
    }

    [HttpPost("{id}/import-draft")]
    public ActionResult<DraftDeckImportResultDto> ImportDraftDeck(long id, DraftDeckImportInputDto input)
    {
      return _userService.ImportDraftDeck(input);
    }
  }
}