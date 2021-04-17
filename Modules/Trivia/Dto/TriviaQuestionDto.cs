using System.Collections.Generic;
using Magicord.Models;

namespace Magicord.Modules.Trivia
{
  public class TriviaQuestionDto
  {
    public string Question { get; set; }
    public IEnumerable<string> Choices { get; set; }
    public string Answer { get; set; }
    public decimal Reward { get; set; }
    public Card CardSubject { get; set; }
    public Set SetSubject { get; set; }
  }
}