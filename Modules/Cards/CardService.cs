using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotChocolate.Execution;
using Magicord.Models;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Cards
{
  public class CardService : ICardService
  {
    private readonly MagicordContext _dataContext;
    private readonly IMapper _mapper;

    public CardService(MagicordContext dataContext, IMapper mapper)
    {
      _dataContext = dataContext;
      _mapper = mapper;
    }

    public IQueryable<Card> GetCardsMatchingQuery(CardSearchInputDto input)
    {
      if (string.IsNullOrEmpty(input.Name))
      {
        throw new QueryException("'Name' argument required.");
      }

      var exactMatch = _dataContext.Cards.Where(x => x.Name.ToLower() == input.Name.ToLower());
      if (!string.IsNullOrEmpty(input.SetCode))
      {
        exactMatch = exactMatch.Where(x => x.SetCode == input.SetCode);
      }

      if (exactMatch.Any())
      {
        return exactMatch;
      }

      if (input.Name.Length < 3)
      {
        throw new QueryException("Card search query too short.");
      }

      // if no exact match, try a broader match
      var matchingCards = _dataContext.Cards
        .Where(x => EF.Functions.ILike(x.Name, $"{input.Name}%"));

      if (!string.IsNullOrEmpty(input.SetCode))
      {
        matchingCards = matchingCards.Where(x => x.SetCode == input.SetCode);
      }

      if (matchingCards.Select(x => x.Name).Distinct().Count() > 1)
      {
        throw new QueryException($"Multiple cards matched {input.Name}. Try being more specific?");
      }

      return matchingCards;
    }
  }
}