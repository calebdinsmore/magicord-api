using System.Collections.Generic;
using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.SealedEvents
{
  public interface ISealedEventService
  {

    IQueryable<SealedEvent> GetActiveSealedEvents();
    List<UserPacks> DistributeUserPacks(long sealedEventId);
    SealedEvent CreateSealedEvent(CreateSealedEventInputDto input);
    SealedEventPack AddPackToSealedEvent(AddPackToSealedInputDto input);
    SealedEventAttendee AddSealedEventAttendee(AddSealedEventAttendeeInputDto input);
  }
}