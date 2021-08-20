using System.Linq;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.SealedEvents
{
  public class AddSealedEventAttendeeInputDto : IValidatable
  {
    public long UserId { get; set; }
    public long SealedEventId { get; set; }
    public bool ProcessFee { get; set; }
    public void Validate(MagicordContext context)
    {
      var user = context.Users.FirstOrDefault(x => x.Id == UserId);
      var sealedEvent = context.SealedEvents.Include(x => x.SealedEventAttendees).FirstOrDefault(x => x.Id == SealedEventId);
      if (user == null)
      {
        throw new QueryException("Couldn't find a matching user in the Magicord system. Have you done `mc start`?");
      }

      if (sealedEvent == null)
      {
        throw new QueryException($"Couldn't find a SealedEvent matching {SealedEventId}");
      }

      if (!sealedEvent.IsActive)
      {
        throw new QueryException("This event is no longer accepting attendees.");
      }

      if (sealedEvent.SealedEventAttendees.Any(x => x.UserId == UserId))
      {
        throw new QueryException("You've already registered for this event.");
      }

      if (ProcessFee)
      {
        if (user.Balance < sealedEvent.EntryFee)
        {
          throw new QueryException($"Insufficient funds to register for event (${sealedEvent.EntryFee} required).");
        }
      }
    }
  }
}