using System.Collections.Generic;
using Magicord.Models.Enums;

namespace Magicord.Modules.AdminProcess
{
  public class CardJson
  {
    public string Artist { get; set; }
    public string AsciiName { get; set; }
    public ICollection<string> Availability { get; set; }
    public string CardKingdomFoilId { get; set; }
    public string CardKingdomId { get; set; }
    public ICollection<string> ColorIdentity { get; set; }
    public ICollection<string> ColorIndicator { get; set; }
    public ICollection<string> Colors { get; set; }
    public double? ConvertedManaCost { get; set; }
    public string DuelDeck { get; set; }
    public long? EdhrecRank { get; set; }
    public double? FaceConvertedManaCost { get; set; }
    public string FaceName { get; set; }
    public string FlavorName { get; set; }
    public string FlavorText { get; set; }
    public ICollection<string> FrameEffects { get; set; }
    public string Hand { get; set; }
    public bool HasAlternativeDeckLimit { get; set; }
    public bool HasContentWarning { get; set; }
    public bool HasFoil { get; set; }
    public bool HasNonFoil { get; set; }
    public Dictionary<string, string> Identifiers { get; set; }
    public bool IsAlternative { get; set; }
    public bool IsFullArt { get; set; }
    public bool IsOnlineOnly { get; set; }
    public bool IsOversized { get; set; }
    public bool IsPromo { get; set; }
    public bool IsReprint { get; set; }
    public bool IsReserved { get; set; }
    public bool IsStarter { get; set; }
    public bool IsStorySpotlight { get; set; }
    public bool IsTextless { get; set; }
    public bool IsTimeshifted { get; set; }
    public ICollection<string> Keywords { get; set; }
    public Dictionary<string, bool> LeadershipSkills { get; set; }
    public string Life { get; set; }
    public string Loyalty { get; set; }
    public string ManaCost { get; set; }
    public string McmId { get; set; }
    public string McmMetaId { get; set; }
    public string MtgArenaId { get; set; }
    public string MtgjsonV4Id { get; set; }
    public string MtgoFoilId { get; set; }
    public string MtgoId { get; set; }
    public string MultiverseId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string OriginalText { get; set; }
    public string OriginalType { get; set; }
    public ICollection<string> OtherFaceIds { get; set; }
    public string Power { get; set; }
    public ICollection<string> Printings { get; set; }
    public ICollection<string> PromoTypes { get; set; }
    public Dictionary<string, string> PurchaseUrls { get; set; }
    public CardsRarity Rarity { get; set; }
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
    public ICollection<string> Variations { get; set; }
    public string Watermark { get; set; }
  }
}