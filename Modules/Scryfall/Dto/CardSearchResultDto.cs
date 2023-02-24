using System.Collections.Generic;

namespace Magicord.Modules.Scryfall
{
  public class CardSearchResultDto
  {
    public int TotalCards { get; set; }
    public List<ScryfallCardDto> Data { get; set; }
  }
}