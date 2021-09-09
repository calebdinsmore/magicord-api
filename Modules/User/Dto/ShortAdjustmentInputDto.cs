using System.Linq;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Users
{
  public class ShortAdjustmentInputDto : IValidatable
  {
    public decimal? ShareAmount { get; set; }
    public decimal? DollarAmount { get; set; }
    public long CardId { get; set; }
    public long UserId { get; set; }
    public bool IsFoil { get; set; }

    public void Validate(MagicordContext context)
    {
      ShareAmount = ShareAmount ?? 0;
      DollarAmount = DollarAmount ?? 0;
      if (ShareAmount > 0 && DollarAmount > 0)
      {
        throw new QueryException("You cannot supply both a share amount and dollar amount. Must be either/or.");
      }

      if (DollarAmount < 0 || ShareAmount < 0)
      {
        throw new QueryException("Dollar amount and share amount must be non-negative");
      }

      if (DollarAmount == 0 && ShareAmount == 0)
      {
        throw new QueryException("Must specify either a share amount or dollar amount.");
      }

      var user = context.Users.FirstOrDefault(x => x.Id == UserId);
      var card = context.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Id == CardId);
      if (user == null || card == null)
      {
        throw new QueryException("Unable to locate either the user or the card ID.");
      }

      if (!context.UserShorts.Any(x => x.CardId == CardId && x.IsFoil == IsFoil))
      {
        throw new QueryException("You don't own a short position in that card.");
      }
    }
  }
}