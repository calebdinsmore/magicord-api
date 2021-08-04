using System.Collections.Generic;

namespace Magicord.Modules.AdminProcess
{
  public class TokenJson
  {
    public string Artist { get; set; }
    public string AsciiName { get; set; }
    public ICollection<string> Availability { get; set; }
    public ICollection<string> ColorIdentity { get; set; }
    public ICollection<string> Colors { get; set; }
    public long? EdhrecRank { get; set; }
    public string FaceName { get; set; }
    public string FlavorText { get; set; }
    public ICollection<string> FrameEffects { get; set; }
    public string FrameVersion { get; set; }
    public bool HasFoil { get; set; }
    public bool HasNonFoil { get; set; }
    public bool IsFullArt { get; set; }
    public bool IsPromo { get; set; }
    public bool IsReprint { get; set; }
    public ICollection<string> Keywords { get; set; }
    public string MtgArenaId { get; set; }
    public string MtgjsonV4Id { get; set; }
    public string MultiverseId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string OriginalText { get; set; }
    public string OriginalType { get; set; }
    public string Power { get; set; }
    public ICollection<string> PromoTypes { get; set; }
    public ICollection<string> ReverseRelated { get; set; }
    public string ScryfallId { get; set; }
    public string ScryfallIllustrationId { get; set; }
    public string ScryfallOracleId { get; set; }
    public string SetCode { get; set; }
    public string Side { get; set; }
    public ICollection<string> Subtypes { get; set; }
    public ICollection<string> Supertypes { get; set; }
    public string TcgplayerProductId { get; set; }
    public string Text { get; set; }
    public string Toughness { get; set; }
    public string Type { get; set; }
    public ICollection<string> Types { get; set; }
    public string Uuid { get; set; }
    public string Watermark { get; set; }
  }
}