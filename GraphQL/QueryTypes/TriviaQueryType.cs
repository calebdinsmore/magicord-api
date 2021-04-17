using HotChocolate;
using Magicord.Modules.Trivia;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    public TriviaQuestionDto GetRandomTrivia([Service] ITriviaGeneratorFactory triviaGeneratorFactory)
    {
      return triviaGeneratorFactory.GetRandomTriviaGenerator().GenerateTriviaQuestion();
    }
  }
}