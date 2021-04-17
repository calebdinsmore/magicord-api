using System;
using System.Collections.Generic;
using System.Linq;
using Magicord.Models;

namespace Magicord.Modules.Trivia
{
  public class FlavorTextTriviaGenerator : ITriviaGenerator
  {
    private MagicordContext _dataContext;
    private Random _random;

    public FlavorTextTriviaGenerator(MagicordContext dataContext, Random random)
    {
      _dataContext = dataContext;
      _random = random;
    }

    public TriviaQuestionDto GenerateTriviaQuestion()
    {
      var card = GetRandomCard();

      return new TriviaQuestionDto
      {
        Question = $"Which flavor text goes with {card.Name} ({card.SetCode})?",
        Answer = card.FlavorText,
        Choices = GetChoices(card),
        Reward = 10,
        CardSubject = card
      };
    }

    private Card GetRandomCard()
    {
      var cardName = _dataContext.Cards
        .Where(x => x.FlavorText != null)
        .Select(x => x.Name)
        .Distinct()
        .OrderBy(x => Guid.NewGuid())
        .Take(1)
        .FirstOrDefault();
      return _dataContext.Cards
      .Where(x => x.Name == cardName && x.FlavorText != null && x.Types != null)
      .FirstOrDefault();
    }

    private List<string> GetChoices(Card card)
    {
      var randomFlavors = _dataContext.Cards
        .Where(x => x.FlavorText != null
          && x.FlavorText != card.FlavorText
          && x.Name != card.Name
          && x.Types == card.Types)
        .Select(x => x.FlavorText)
        .Distinct()
        .OrderBy(x => Guid.NewGuid())
        .Take(3)
        .ToList();
      randomFlavors.Add(card.FlavorText);
      return randomFlavors.OrderBy(x => _random.Next()).ToList();
    }
  }
}