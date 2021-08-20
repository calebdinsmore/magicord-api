using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Magicord.Models;
using Magicord.Modules.SealedEvents;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    [UseProjection]
    public IQueryable<SealedEvent> GetActiveSealedEvents([Service] ISealedEventService service)
    {
      return service.GetActiveSealedEvents();
    }
  }
}