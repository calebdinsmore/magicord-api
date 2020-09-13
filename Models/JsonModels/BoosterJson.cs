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
    public Sheets Sheets { get; set; }
    public IEnumerable<BoosterContentConfig> Boosters { get; set; }
  }

  public class BoosterContentConfig
  {
    public int Weight { get; set; }
    public BoosterSlots Contents { get; set; }
  }

  public class BoosterSlots
  {
    public int Common { get; set; }
    public int Uncommon { get; set; }
    public int Rare { get; set; }
    public int RareMythic { get; set; }
    public int Foil { get; set; }
    public int Basic { get; set; }
  }

  public class Sheets
  {
    public Sheet Common { get; set; }
    public Sheet Uncommon { get; set; }
    public Sheet Rare { get; set; }
    public Sheet RareMythic { get; set; }
  }

  public class Sheet
  {
    public bool Foil { get; set; }
    public Dictionary<string, int> Cards { get; set; }
    public int TotalWeight { get; set; }
    public bool BalanceColors { get; set; }
  }
}
