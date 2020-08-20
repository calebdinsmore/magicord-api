using System;
using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class SetTranslation : IEntity
  {
    public long Id { get; set; }
    public string SetCode { get; set; }
    public string Translation { get; set; }

    public virtual Set SetCodeNavigation { get; set; }
  }
}
