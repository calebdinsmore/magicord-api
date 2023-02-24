using System.Threading.Tasks;
using HotChocolate;
using Magicord.Modules.Trivia;

namespace Magicord.GraphQL.QueryTypes
{
  public partial class Query
  {
    public async Task<TriviaQuestionDto> GetRandomTrivia([Service] ITriviaGeneratorFactory triviaGeneratorFactory)
    {
      return await triviaGeneratorFactory.GetRandomTriviaGenerator().GenerateTriviaQuestion();
    }
  }
}