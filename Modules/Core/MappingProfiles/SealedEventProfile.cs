using AutoMapper;
using Magicord.Models;
using Magicord.Modules.SealedEvents;

namespace Magicord.Modules.Core.MappingProfiles
{
  public class SealedEventProfile : Profile
  {
    public SealedEventProfile()
    {
      CreateMap<SealedEvent, CreateSealedEventInputDto>().ReverseMap();
      CreateMap<SealedEventPack, AddPackToSealedInputDto>().ReverseMap();
      CreateMap<SealedEventAttendee, AddSealedEventAttendeeInputDto>().ReverseMap();
    }
  }
}