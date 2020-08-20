using System;
using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class Ruling : IEntity
  {
    public long Id { get; set; }
    public DateTime? Date { get; set; }
    public string Text { get; set; }
    public string Uuid { get; set; }

    public virtual Card Uu { get; set; }
  }
}
