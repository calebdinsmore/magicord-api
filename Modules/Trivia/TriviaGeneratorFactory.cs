using System;
using Magicord.Models;

namespace Magicord.Modules.Trivia
{
  public class TriviaGeneratorFactory : ITriviaGeneratorFactory
  {
    private MagicordContext _dataContext;
    private Random _random;
    public TriviaGeneratorFactory(MagicordContext dataContext, Random random)
    {
      _dataContext = dataContext;
      _random = random;
    }
    public ITriviaGenerator GetRandomTriviaGenerator()
    {
      switch (_random.Next(2))
      {
        case 0:
          return new ColorIdentityTriviaGenerator(_dataContext, _random);
        case 1:
          return new FlavorTextTriviaGenerator(_dataContext, _random);
          // case 2:
          // return new ReprintTriviaGenerator(_dataContext, _random);
      }

      return new ColorIdentityTriviaGenerator(_dataContext, _random);
    }
  }
}