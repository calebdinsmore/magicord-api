namespace Magicord.Models
{
  public class UserCard
  {
    public long UserId { get; set; }
    public string CardUuid { get; set; }
    public int Quantity { get; set; }
    public int AmountFoil { get; set; }

    public virtual User User { get; set; }
    public virtual Card Card { get; set; }
  }
}