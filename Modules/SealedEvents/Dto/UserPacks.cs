using System.Collections.Generic;
using Magicord.Modules.Booster;

namespace Magicord.Modules.SealedEvents
{
  public class UserPacks
  {
    public long UserId { get; set; }
    public List<BoosterPackDto> Packs { get; set; }
  }
}