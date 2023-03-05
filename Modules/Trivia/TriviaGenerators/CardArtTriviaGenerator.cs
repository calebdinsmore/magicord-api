using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magicord.Modules.Scryfall;

namespace Magicord.Modules.Trivia
{
  public class CardArtTriviaGenerator : ITriviaGenerator
  {
    private static readonly List<string> ArtTags = new List<string>(new[] { "axe", "boat", "bone", "rat", "orb", "dragon", "horse", "explosion" });
    private IScryfallService _scryfallService;
    private Random _random;

    public CardArtTriviaGenerator(IScryfallService scryfallService, Random random)
    {
      _scryfallService = scryfallService;
      _random = random;
    }

    public async Task<TriviaQuestionDto> GenerateTriviaQuestion()
    {
      var triviaQuestionDto = new TriviaQuestionDto { Reward = 15 };

      var tag = ArtTags[_random.Next(ArtTags.Count)];
      var result = await _scryfallService.SearchCards($"art:{tag}");
      var correctIndex = _random.Next(result.Data.Count);
      var correctAnswerCard = result.Data[correctIndex];
      var correctAnswer = correctAnswerCard.Name;
      result.Data.RemoveAt(correctIndex);
      var answers = new List<string>(new []{ correctAnswer });
      for (var i = 0; i < 3; i++)
      {
        var nextChoiceIndex = _random.Next(result.Data.Count);
        answers.Add(result.Data[nextChoiceIndex].Name);
        result.Data.RemoveAt(nextChoiceIndex);
      }

      triviaQuestionDto.Answer = correctAnswer;
      triviaQuestionDto.Choices = answers;
      triviaQuestionDto.Question = "Which card has this art?";
      triviaQuestionDto.ImageUri = correctAnswerCard.image_uris.GetValueOrDefault("art_crop");
      return triviaQuestionDto;
    }
  }
}