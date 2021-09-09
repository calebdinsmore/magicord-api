using System;
using System.Collections.Generic;

namespace Magicord.Modules.AdminProcess
{
  public class AllPricesJson
  {
    public Dictionary<string, FullCardPriceData> Data { get; set; }
  }

  public class FullCardPriceData
  {
    public PaperRetailerJson Paper { get; set; }
  }

  public class PaperRetailerJson
  {
    public RetailerPriceListJson CardKingdom { get; set; }
    public RetailerPriceListJson TcgPlayer { get; set; }
  }

  public class RetailerPriceListJson
  {
    public PriceTypeJson Buylist { get; set; }
    public PriceTypeJson Retail { get; set; }
  }

  public class PriceTypeJson
  {
    public SortedDictionary<DateTime, decimal> Foil { get; set; }
    public SortedDictionary<DateTime, decimal> Normal { get; set; }
  }
}