namespace Magicord.Models
{
  public partial class SealedEventPromo
  {
    public long Id { get; set; }
    public long SealedEventId { get; set; }
    public string PromoType { get; set; }
    public string SetCode { get; set; }

    public virtual SealedEvent SealedEvent { get; set; }
  }
}