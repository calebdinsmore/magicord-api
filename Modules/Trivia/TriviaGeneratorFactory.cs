using System;
using Magicord.Models;
using Magicord.Modules.Scryfall;

namespace Magicord.Modules.Trivia
{
  public class TriviaGeneratorFactory : ITriviaGeneratorFactory
  {
    private MagicordContext _dataContext;
    private Random _random;
    private IScryfallService _scryfallService;
    public TriviaGeneratorFactory(MagicordContext dataContext, Random random, IScryfallService scryfallService)
    {
      _dataContext = dataContext;
      _random = random;
      _scryfallService = scryfallService;
    }
    public ITriviaGenerator GetRandomTriviaGenerator()
    {
      switch (_random.Next(3))
      {
        case 0:
          return new ColorIdentityTriviaGenerator(_dataContext, _random);
        case 1:
          return new FlavorTextTriviaGenerator(_dataContext, _random);
        case 2:
          return new CardArtTriviaGenerator(_scryfallService, _random);
      }

      return new ColorIdentityTriviaGenerator(_dataContext, _random);
    }
  }
}