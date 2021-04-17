using System;
using System.Collections.Generic;
using System.Linq;
using Magicord.Models;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Trivia
{
  public class ColorIdentityTriviaGenerator : ITriviaGenerator
  {
    private Dictionary<int, List<string>> IDENTITY_MAP;

    private MagicordContext _dataContext;
    private Random _random;

    public ColorIdentityTriviaGenerator(MagicordContext dataContext, Random random)
    {
      _dataContext = dataContext;
      _random = random;
      InitConstants();
    }

    public TriviaQuestionDto GenerateTriviaQuestion()
    {
      var triviaQuestionDto = new TriviaQuestionDto();
      Card card;
      if (_random.Next(4) == 0)
      {
        // 25%
        card = GetRandomMismatchedCard();
        triviaQuestionDto.Reward = 15;
      }
      else
      {
        // 75%
        card = GetRandomCardAboveThreeDollars();
        triviaQuestionDto.Reward = 10;
      }
      triviaQuestionDto.CardSubject = card;
      triviaQuestionDto.Question = $"What is {card.Name}'s color identity?";
      triviaQuestionDto.Choices = GenerateRandomIncorrectAnswers(card.ColorIdentity);
      triviaQuestionDto.Answer = card.ColorIdentity;
      return triviaQuestionDto;
    }

    private Card GetRandomCardAboveThreeDollars()
    {
      var cardName = _dataContext.Cards.Include(x => x.CardPrice)
        .Where(x => x.ColorIdentity != null && x.CardPrice.CurrentRetailNonFoil > 3)
        .Select(x => x.Name)
        .Distinct()
        .OrderBy(x => Guid.NewGuid())
        .Take(1)
        .FirstOrDefault();
      return _dataContext.Cards.Where(x => x.Name == cardName).FirstOrDefault();
    }

    private Card GetRandomMismatchedCard()
    {
      var cardName = _dataContext.Cards
        .Where(x => x.ColorIdentity != x.Colors)
        .Select(x => x.Name)
        .Distinct()
        .OrderBy(x => Guid.NewGuid())
        .Take(1)
        .FirstOrDefault();
      return _dataContext.Cards.Where(x => x.Name == cardName).FirstOrDefault();
    }

    private List<string> GenerateRandomIncorrectAnswers(string correctAnswer)
    {
      var answers = new List<string>(new[] { correctAnswer });
      var numColors = correctAnswer.Split(',').Count();
      List<string> identityList;
      switch (numColors)
      {
        case 1:
        case 2:
          identityList = IDENTITY_MAP[1].Concat(IDENTITY_MAP[2]).ToList();
          break;
        case 3:
        case 4:
        case 5:
          identityList = IDENTITY_MAP[3].Concat(IDENTITY_MAP[4]).Concat(IDENTITY_MAP[5]).ToList();
          break;
        default:
          identityList = IDENTITY_MAP[3].ToList();
          break;
      }
      if (numColors == 5)
      {
        identityList = identityList.Concat(IDENTITY_MAP[4]).ToList();
      }
      identityList = identityList.Where(x => x != correctAnswer).ToList();
      for (var i = 0; i < 3; i++)
      {
        var randomIndex = _random.Next(identityList.Count);
        answers.Add(identityList[randomIndex]);
        identityList.RemoveAt(randomIndex);
      }
      return answers.OrderBy(x => _random.Next()).ToList();
    }

    private void InitConstants()
    {
      IDENTITY_MAP = new Dictionary<int, List<string>>();
      IDENTITY_MAP.Add(1, new List<string>(new[] { "B", "G", "R", "U", "W" }));
      IDENTITY_MAP.Add(2, new List<string>(new[] { "B,G", "B,R", "B,U", "B,W", "G,R", "G,U", "G,W", "R,U", "R,W", "U,W" }));
      IDENTITY_MAP.Add(3, new List<string>(new[] { "B,G,R", "B,G,U", "B,G,W", "B,R,U", "B,R,W", "B,U,W", "G,R,U", "G,R,W", "G,U,W", "R,U,W" }));
      IDENTITY_MAP.Add(4, new List<string>(new[] { "B,G,R,U", "B,G,R,W", "B,G,U,W", "B,R,U,W", "G,R,U,W" }));
      IDENTITY_MAP.Add(5, new List<string>(new[] { "B,G,R,U,W" }));
    }
  }
}