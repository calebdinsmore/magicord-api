using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicord.Models;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Trivia
{
  public class ReprintTriviaGenerator : ITriviaGenerator
  {
    private MagicordContext _dataContext;
    private Random _random;

    public ReprintTriviaGenerator(MagicordContext dataContext, Random random)
    {
      _dataContext = dataContext;
      _random = random;
    }

    public async Task<TriviaQuestionDto> GenerateTriviaQuestion()
    {
      var card = GetRandomCard();
      var correctSet = GetCorrectSet(card);
      return new TriviaQuestionDto
      {
        Question = $"In what set was {card.Name} first reprinted?",
        Answer = $"{correctSet.Name} ({correctSet.Code})",
        Choices = GetChoices(correctSet, card),
        Reward = 18,
        CardSubject = card,
        SetSubject = correctSet
      };
    }

    private Card GetRandomCard()
    {
      return _dataContext.Cards.Include(x => x.SetCodeNavigation).Include(x => x.CardPrice)
      .Where(x => x.Printings.Length > 11 && x.CardPrice.CurrentRetailNonFoil > 3 && !x.Type.Contains("Basic Land"))
      .OrderBy(x => Guid.NewGuid())
      .Take(1).FirstOrDefault();
    }

    private Set GetCorrectSet(Card card)
    {
      var firstReprint = card.Printings.Split(',')[1];
      var set = _dataContext.Sets.FirstOrDefault(x => x.Code == firstReprint);
      return set;
    }

    private IEnumerable<string> GetChoices(Set correctSet, Card card)
    {
      var printings = card.Printings.Split(',').ToList();
      var correctSetCode = printings[1];
      var choices = new List<Set>();
      var randomSets = _dataContext.Sets
        .Where(x => x.ReleaseDate > correctSet.ReleaseDate.Value.AddYears(-2) && x.ReleaseDate < correctSet.ReleaseDate.Value.AddYears(2) && x.Code != correctSet.Code && !printings.Contains(x.Code)).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

      printings = printings.Where(x => x != correctSetCode).OrderBy(x => _random.Next()).ToList();
      for (var i = 0; i < randomSets.Count; i++)
      {
        if (_random.Next(2) == 0)
        {
          choices.Add(randomSets[i]);
        }
        else
        {
          if (printings.Count > i)
          {
            choices.Add(_dataContext.Sets.FirstOrDefault(x => x.Code == printings[i]));
          }
          else
          {
            choices.Add(randomSets[i]);
          }
        }
      }
      choices.Add(correctSet);
      return choices.OrderBy(x => _random.Next()).Select(x => $"{x.Name} ({x.Code})");
    }
  }
}