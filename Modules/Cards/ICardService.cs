using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicord.Models;

namespace Magicord.Modules.Cards
{
  public interface ICardService
  {
    Task<IQueryable<Card>> GetCardsMatchingQuery(CardSearchInputDto input);
  }
}