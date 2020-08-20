using System;
using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class Meta : IEntity
  {
    public long Id { get; set; }
    public DateTime? Date { get; set; }
    public string Version { get; set; }
  }
}
