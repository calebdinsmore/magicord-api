using System.Collections.Generic;

namespace Magicord.Modules.Booster
{
  public class BoosterStatsDto
  {
    public string SetCode { get; set; }
    public decimal AverageBuylistPrice { get; set; }
    public decimal AverageRetailPrice { get; set; }
    public List<decimal> BuylistPrices { get; set; }
    public List<decimal> RetailPrices { get; set; }
  }
}