using System.Collections.Generic;
using Magicord.Models;

namespace Magicord.Modules.Shop
{
  public class BuylistCardResultDto
  {
    public Card Card { get; set; }
    public List<UserCard> CardSuggestions { get; set; }
  }
}