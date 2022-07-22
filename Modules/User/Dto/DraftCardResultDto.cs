using Magicord.Models.Enums;

namespace Magicord.Modules.Users
{
  public class DraftCardResultDto
  {
    public string ScryfallId { get; set; }
    public string Name { get; set; }
    public CardsRarity Rarity { get; set; }
    public string Side { get; set; }
  }
}