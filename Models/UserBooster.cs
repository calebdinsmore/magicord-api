using System.Collections.Generic;

namespace Magicord.Models
{
  public class UserBooster : IEntity
  {
    public long Id { get; set; }
    public long UserId { get; set; }
    public string SetCode { get; set; }
    public decimal BuyPrice { get; set; }
    public bool IsOpened { get; set; }

    public virtual User User { get; set; }
    public virtual Set Set { get; set; }
    public virtual ICollection<UserBoosterCard> UserBoosterCards { get; set; }
  }
}
