namespace Magicord.Modules.Shop
{
  public class PurchaseRequestDto
  {
    public long PurchaserId { get; set; }
    public long ItemId { get; set; }
    public int Quantity { get; set; }
    public bool OpenProduct { get; set; }
  }
}