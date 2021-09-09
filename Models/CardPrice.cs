using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Magicord.Models
{
  public class CardPrice : IEntity
  {
    public CardPriceHistory LastPrice
    {
      get
      {
        if ((CardPriceHistories?.Count ?? 0) == 0)
        {
          return null;
        }
        var descending = CardPriceHistories.OrderByDescending(x => x.DateRecorded);
        return descending.ToArray()[0];
      }
    }

    public long Id { get; set; }
    public string CardUuid { get; set; }
    public decimal CurrentBuylistFoil { get; set; }
    public decimal CurrentBuylistNonFoil { get; set; }
    public decimal CurrentRetailFoil { get; set; }
    public decimal CurrentRetailNonFoil { get; set; }
    [Column(TypeName = "jsonb")]
    public Dictionary<DateTime, decimal> BuylistFoilHistory { get; set; }
    [Column(TypeName = "jsonb")]
    public Dictionary<DateTime, decimal> BuylistNonFoilHistory { get; set; }
    [Column(TypeName = "jsonb")]
    public Dictionary<DateTime, decimal> RetailFoilHistory { get; set; }
    [Column(TypeName = "jsonb")]
    public Dictionary<DateTime, decimal> RetailNonFoilHistory { get; set; }

    public virtual Card Card { get; set; }
    public virtual ICollection<CardPriceHistory> CardPriceHistories { get; set; }
  }
}
