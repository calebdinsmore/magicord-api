namespace Magicord.Models
{
  public partial class SealedEventPack : IEntity
  {
    public long Id { get; set; }
    public string SetCode { get; set; }
    public string BoosterType { get; set; }
    public int PackCount { get; set; }
    public long SealedEventId { get; set; }

    public virtual SealedEvent SealedEvent { get; set; }
    public virtual Set Set { get; set; }
  }
}