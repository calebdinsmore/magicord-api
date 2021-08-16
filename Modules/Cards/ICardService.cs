using System.Collections.Generic;
using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Cards
{
  public interface ICardService
  {
    IQueryable<Card> GetCardsMatchingQuery(CardSearchInputDto input);
  }
}