using System.Linq;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.SealedEvents
{
  public class AddPromoToSealedInputDto : IValidatable
  {
    public long SealedEventId { get; set; }
    public string PromoType { get; set; }
    public string SetCode { get; set; }

    public void Validate(MagicordContext context)
    {
      if (!context.SealedEvents.Any(x => x.Id == SealedEventId))
      {
        throw new QueryException("Couldn't find a sealed event with that ID");
      }

      if (!context.Sets.Any(x => x.Code.ToLower() == SetCode.ToLower()))
      {
        throw new QueryException("Couldn't find a set matching that set code.");
      }

      if (PromoType != null && !context.Cards.Where(x => x.SetCode.ToLower() == SetCode.ToLower())
            .Any(x => EF.Functions.ILike(x.PromoTypes, $"%{PromoType}%")))
      {
        throw new QueryException($"There aren't cards matching that promo type in {SetCode}.");
      }
    }
  }
}