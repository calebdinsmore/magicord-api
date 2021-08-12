namespace Magicord.Modules.Shop
{
  public class BuylistCardInputDto
  {
    public long UserId { get; set; }
    public string CardName { get; set; }
    public long CardId { get; set; }
    public int Amount { get; set; }
    public int AmountFoil { get; set; }
  }
}