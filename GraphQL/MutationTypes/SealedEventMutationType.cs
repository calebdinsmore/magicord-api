using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.SealedEvents;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public SealedEvent CreateSealedEvent([Service] ISealedEventService service, CreateSealedEventInputDto input)
    {
      return service.CreateSealedEvent(input);
    }

    public List<UserPacks> DistributeUserPacks([Service] ISealedEventService service, long sealedEventId)
    {
      return service.DistributeUserPacks(sealedEventId);
    }

    public SealedEventPack AddPackToSealedEvent([Service] ISealedEventService service, AddPackToSealedInputDto input)
    {
      return service.AddPackToSealedEvent(input);
    }

    public SealedEventAttendee AddSealedEventAttendee([Service] ISealedEventService service, AddSealedEventAttendeeInputDto input)
    {
      return service.AddSealedEventAttendee(input);
    }
  }
}