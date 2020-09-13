namespace Magicord.Models
{
  public class UserBoosterCard : IEntity
  {
    public long Id { get; set; }
    public long UserBoosterId { get; set; }
    public string CardUuid { get; set; }

    public virtual Card Card { get; set; }
    public virtual UserBooster UserBooster { get; set; }
  }
}
