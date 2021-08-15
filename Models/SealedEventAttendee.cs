namespace Magicord.Models
{
  public partial class SealedEventAttendee
  {
    public long UserId { get; set; }
    public long SealedEventId { get; set; }

    public virtual SealedEvent SealedEvent { get; set; }
    public virtual User User { get; set; }
  }
}