using AutoMapper;
using Magicord.Modules.Users.Dto;
using Magicord.Models;

namespace Magicord.Modules.Core.MappingProfiles
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, UserDto>().ReverseMap();
    }
  }
}