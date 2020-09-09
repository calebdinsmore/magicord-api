using System.Collections.Generic;

namespace Magicord.Models
{
  public class Booster : IEntity
  {
    public long Id { get; set; }
    public string SetCode { get; set; }
    public decimal BuyPrice { get; set; }
    public bool IsOpened { get; set; }

    public virtual Set Set { get; set; }
    public virtual ICollection<UserBooster> UserBoosters { get; set; }
    public virtual ICollection<BoosterCard> BoosterCards { get; set; }
  }
}
