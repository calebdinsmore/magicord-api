using HotChocolate;

namespace Magicord.Models
{
  public partial class UserShare : IEntity
  {
    public decimal CurrentValue
    {
      get
      {
        var shareValue = IsFoil ? Card?.CardPrice?.CurrentRetailFoil ?? 0 : Card?.CardPrice?.CurrentRetailNonFoil ?? 0;
        return shareValue;
      }
    }

    public decimal GainOrLossPercent
    {
      get
      {
        if (AverageInvestedValue == 0)
        {
          return 0;
        }
        return (CurrentValue - AverageInvestedValue) / AverageInvestedValue;
      }
    }

    public decimal GainOrLoss
    {
      get
      {
        return (CurrentValue * Amount) - (AverageInvestedValue * Amount);
      }
    }

    public long Id { get; set; }
    public long CardId { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal CashInvested { get; set; }
    public decimal AverageInvestedValue { get; set; }
    public bool IsFoil { get; set; }
    [GraphQLIgnore]
    public uint xmin { get; set; }

    public virtual Card Card { get; set; }
    public virtual User User { get; set; }
  }
}