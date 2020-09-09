namespace Magicord.Models
{
  public class UserBooster : IEntity
  {
    public long Id { get; set; }
    public long UserId { get; set; }
    public long BoosterId { get; set; }

    public virtual User User { get; set; }
    public virtual Booster Booster { get; set; }
  }
}
