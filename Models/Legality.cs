using System;
using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class Legality : IEntity
  {
    public long Id { get; set; }
    public string Uuid { get; set; }

    public virtual Card Uu { get; set; }
  }
}
