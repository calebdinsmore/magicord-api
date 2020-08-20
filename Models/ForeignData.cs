using System;
using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class ForeignData : IEntity
  {
    public long Id { get; set; }
    public string FlavorText { get; set; }
    public long? Multiverseid { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public string Uuid { get; set; }

    public virtual Card Uu { get; set; }
  }
}
