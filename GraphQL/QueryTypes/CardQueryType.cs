using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using Magicord.Models;
using Magicord.Modules.Cards;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    [UseProjection]
    public Task<IQueryable<Card>> GetMatchingCards([Service] ICardService cardService, CardSearchInputDto input)
    {
      return cardService.GetCardsMatchingQuery(input);
    }
  }
}