using System;
using System.Collections.Generic;
using Magicord.Models.Enums;

namespace Magicord.Models
{
  public partial class Card : IEntity
  {
    public Card()
    {
      ForeignDatas = new HashSet<ForeignData>();
      Legalities = new HashSet<Legality>();
      Rulings = new HashSet<Ruling>();
    }

    public long Id { get; set; }
    public string Artist { get; set; }
    public string AsciiName { get; set; }
    public string Availability { get; set; }
    public string CardKingdomFoilId { get; set; }
    public string CardKingdomId { get; set; }
    public string ColorIdentity { get; set; }
    public string ColorIndicator { get; set; }
    public string Colors { get; set; }
    public double? ConvertedManaCost { get; set; }
    public string DuelDeck { get; set; }
    public long? EdhrecRank { get; set; }
    public double? FaceConvertedManaCost { get; set; }
    public string FaceName { get; set; }
    public string FlavorName { get; set; }
    public string FlavorText { get; set; }
    public string FrameEffects { get; set; }
    public string Hand { get; set; }
    public bool HasAlternativeDeckLimit { get; set; }
    public bool HasContentWarning { get; set; }
    public bool HasFoil { get; set; }
    public bool HasNonFoil { get; set; }
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
    public string Keywords { get; set; }
    public string LeadershipSkills { get; set; }
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
    public string OtherFaceIds { get; set; }
    public string Power { get; set; }
    public string Printings { get; set; }
    public string PromoTypes { get; set; }
    public string PurchaseUrls { get; set; }
    public CardsRarity Rarity { get; set; }
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
    public string Variations { get; set; }
    public string Watermark { get; set; }

    public virtual Set SetCodeNavigation { get; set; }
    public virtual ICollection<ForeignData> ForeignDatas { get; set; }
    public virtual ICollection<Legality> Legalities { get; set; }
    public virtual ICollection<Ruling> Rulings { get; set; }
    public virtual ICollection<UserCard> UserCards { get; set; }
    public virtual ICollection<UserBoosterCard> UserBoosterCards { get; set; }
    public virtual ICollection<CardPriceHistory> CardPriceHistories { get; set; }
    public virtual CardPrice CardPrice { get; set; }
  }
}
