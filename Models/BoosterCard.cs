namespace Magicord.Models
{
  public class BoosterCard : IEntity
  {
    public long Id { get; set; }
    public long BoosterId { get; set; }
    public string CardUuid { get; set; }

    public virtual Card Card { get; set; }
    public virtual Booster Booster { get; set; }
  }
}
