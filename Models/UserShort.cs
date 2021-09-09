using HotChocolate;

namespace Magicord.Models
{
  public partial class UserShort : IEntity
  {
    public bool IsRed
    {
      get
      {
        return (BuybackCost * Amount) > ReservedCash;
      }
    }

    public decimal BuybackCost
    {
      get
      {
        return IsFoil ? (Card?.CardPrice?.CurrentRetailFoil ?? 0) : (Card?.CardPrice?.CurrentRetailNonFoil ?? 0);
      }
    }

    public decimal GainOrLoss
    {
      get
      {
        return (ShortedValue - BuybackCost) * Amount;
      }
    }

    public long Id { get; set; }
    public long CardId { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal ReservedCash { get; set; }
    public bool IsFoil { get; set; }
    public decimal ShortedValue { get; set; }
    [GraphQLIgnore]
    public uint xmin { get; set; }

    public virtual Card Card { get; set; }
    public virtual User User { get; set; }
  }
}