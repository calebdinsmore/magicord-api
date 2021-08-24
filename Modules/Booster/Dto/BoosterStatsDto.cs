using System;
using System.Collections.Generic;
using System.Linq;

namespace Magicord.Modules.Booster
{
  public class BoosterStatsDto
  {
    public string SetCode { get; set; }
    public decimal AverageBuylistPrice { get; set; }
    public decimal AverageRetailPrice { get; set; }
    public List<decimal> BuylistPrices { get; set; }
    public List<decimal> RetailPrices { get; set; }
    public decimal BuylistAvg
    {
      get
      {
        if (!BuylistPrices.Any())
        {
          return 0;
        }
        return BuylistPrices.Sum() / BuylistPrices.Count;
      }
    }

    public double BuylistStDev
    {
      get
      {
        if (BuylistPrices.Count < 2)
        {
          return 0;
        }
        var avg = (double)BuylistAvg;
        var sum = BuylistPrices.Sum(d => Math.Pow((double)d - avg, 2));
        return Math.Sqrt(sum / (BuylistPrices.Count - 1));
      }
    }

    public decimal BuylistMin
    {
      get
      {
        return BuylistPrices.Min();
      }
    }

    public decimal BuylistMax
    {
      get
      {
        return BuylistPrices.Max();
      }
    }

    public decimal MinSuggestedPrice
    {
      get
      {
        var byFour = AverageBuylistPrice * 4;
        var roundedUp = Math.Ceiling(byFour);
        return roundedUp / 4;
      }
    }

    public decimal MaxSuggestedPrice
    {
      get
      {
        return MinSuggestedPrice + 0.25m;
      }
    }

    public string BuylistProfitProbability
    {
      get
      {
        if (!BuylistPrices.Any())
        {
          return "NA";
        }

        var countAboveMinSuggested = BuylistPrices.Count(x => x > MinSuggestedPrice);
        var countAboveMaxSuggested = BuylistPrices.Count(x => x > MaxSuggestedPrice);
        var test = countAboveMinSuggested / BuylistPrices.Count;

        return $"${MinSuggestedPrice}: {Decimal.Divide(countAboveMinSuggested, BuylistPrices.Count) * 100m}%\n"
             + $"${MaxSuggestedPrice}: {Decimal.Divide(countAboveMaxSuggested, BuylistPrices.Count) * 100m}%";
      }
    }
  }
}