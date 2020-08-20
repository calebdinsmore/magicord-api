using System;
using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class Token : IEntity
  {
    public long Id { get; set; }
    public string Artist { get; set; }
    public string AsciiName { get; set; }
    public string Availability { get; set; }
    public string ColorIdentity { get; set; }
    public string Colors { get; set; }
    public long? EdhrecRank { get; set; }
    public string FaceName { get; set; }
    public string FlavorText { get; set; }
    public string FrameEffects { get; set; }
    public string FrameVersion { get; set; }
    public bool HasFoil { get; set; }
    public bool HasNonFoil { get; set; }
    public bool IsFullArt { get; set; }
    public bool IsPromo { get; set; }
    public bool IsReprint { get; set; }
    public string Keywords { get; set; }
    public string MtgArenaId { get; set; }
    public string MtgjsonV4Id { get; set; }
    public string MultiverseId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string OriginalText { get; set; }
    public string OriginalType { get; set; }
    public string Power { get; set; }
    public string PromoTypes { get; set; }
    public string ReverseRelated { get; set; }
    public string ScryfallId { get; set; }
    public string ScryfallIllustrationId { get; set; }
    public string ScryfallOracleId { get; set; }
    public string SetCode { get; set; }
    public string Side { get; set; }
    public string Subtypes { get; set; }
    public string Supertypes { get; set; }
    public string TcgplayerProductId { get; set; }
    public string Text { get; set; }
    public string Toughness { get; set; }
    public string Type { get; set; }
    public string Types { get; set; }
    public string Uuid { get; set; }
    public string Watermark { get; set; }

    public virtual Set SetCodeNavigation { get; set; }
  }
}
