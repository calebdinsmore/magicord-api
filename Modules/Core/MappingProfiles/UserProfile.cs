using AutoMapper;
using Magicord.Modules.Users.Dto;
using Magicord.Models;
using Magicord.Modules.Users;

namespace Magicord.Modules.Core.MappingProfiles
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, UserDto>().ReverseMap();
      CreateMap<User, CreateUserInputDto>().ReverseMap();
      CreateMap<Card, DraftCardResultDto>().ReverseMap();
      CreateMap<Token, DraftTokenResultDto>().ReverseMap();
    }
  }
}