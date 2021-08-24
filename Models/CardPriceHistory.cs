using System;

namespace Magicord.Models
{
  public partial class CardPriceHistory : IEntity
  {
    public long Id { get; set; }
    public string CardUuid { get; set; }
    public decimal BuylistFoil { get; set; }
    public decimal BuylistNonFoil { get; set; }
    public decimal RetailFoil { get; set; }
    public decimal RetailNonFoil { get; set; }
    public DateTime DateRecorded { get; set; }
    public long CardPriceId { get; set; }
    public virtual Card Card { get; set; }
    public virtual CardPrice CardPrice { get; set; }
  }
}