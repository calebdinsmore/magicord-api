using System.Collections.Generic;

namespace Magicord.Modules.Scryfall
{
  public class ScryfallCardDto
  {
    public Dictionary<string, string> image_uris { get; set; }
    public string Name { get; set; }
  }
}