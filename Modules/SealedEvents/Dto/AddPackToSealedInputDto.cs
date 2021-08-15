using System.Linq;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.SealedEvents
{
  public class AddPackToSealedInputDto : IValidatable
  {
    public string SetCode { get; set; }
    public string BoosterType { get; set; }
    public int PackCount { get; set; }
    public long SealedEventId { get; set; }

    public void Validate(MagicordContext context)
    {
      if (!context.SealedEvents.Any(x => x.Id == SealedEventId))
      {
        throw new QueryException($"Couldn't find a SealedEvent matching {SealedEventId}");
      }

      var set = context.Sets.FirstOrDefault(x => x.Code == SetCode);
      if (set == null || set.Booster == null)
      {
        throw new QueryException($"Couldnt find a set with a booster definition matching {SetCode}.");
      }

      if (PackCount < 0)
      {
        throw new QueryException("PackCount cannot be negative.");
      }
    }
  }
}