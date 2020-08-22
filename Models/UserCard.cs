namespace Magicord.Models
{
  public class UserCard
  {
    public long UserId { get; set; }
    public long CardId { get; set; }

    public virtual User User { get; set; }
    public virtual Card Card { get; set; }
  }
}