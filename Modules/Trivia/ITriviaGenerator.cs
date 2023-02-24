using System.Threading.Tasks;

namespace Magicord.Modules.Trivia
{
  public interface ITriviaGenerator
  {
    Task<TriviaQuestionDto> GenerateTriviaQuestion();
  }
}