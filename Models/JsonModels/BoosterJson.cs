using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magicord.Models
{
  public class BoosterJson
  {
    public BoosterConfig Default { get; set; }
    public BoosterConfig Premium { get; set; }
  }

  public class BoosterConfig
  {
    // string key corresponds to sheet name
    public Dictionary<string, Sheet> Sheets { get; set; }
    public IEnumerable<BoosterContentConfig> Boosters { get; set; }
    public int BoostersTotalWeight { get; set; }
  }

  public class BoosterContentConfig
  {
    public int Weight { get; set; }
    // key is card UUID
    public Dictionary<string, int> Contents { get; set; }
  }

  public class Sheet
  {
    public bool Foil { get; set; }
    // string key: card UUID. 
    // int value: number of cards from sheet
    public Dictionary<string, int> Cards { get; set; }
    public int TotalWeight { get; set; }
    public bool BalanceColors { get; set; }
  }
}
