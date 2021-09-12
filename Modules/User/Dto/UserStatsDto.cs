namespace Magicord.Modules.Users
{
  public class UserStatsDto
  {
    public decimal Balance { get; set; }
    public decimal NetWorth
    {
      get
      {
        return Balance + BuylistValue + LongPositionValue + ShortPositionValue;
      }
    }
    public int NumberOfCardsOwned { get; set; }
    public decimal BuylistValue { get; set; }
    public decimal LongPositionValue { get; set; }
    public decimal ShortPositionValue { get; set; }
  }
}