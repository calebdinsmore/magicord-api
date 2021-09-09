using HotChocolate;

namespace Magicord.Models
{
  public partial class UserShort : IEntity
  {
    public bool IsRed
    {
      get
      {
        return BuybackCost > ReservedCash;
      }
    }

    public decimal BuybackCost
    {
      get
      {
        return IsFoil ? (Card?.CardPrice?.CurrentRetailFoil ?? 0) : (Card?.CardPrice?.CurrentRetailNonFoil ?? 0) * Amount;
      }
    }

    public decimal GainOrLoss
    {
      get
      {
        return (ReservedCash / 2) - BuybackCost;
      }
    }

    public long Id { get; set; }
    public long CardId { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal ReservedCash { get; set; }
    public bool IsFoil { get; set; }
    [GraphQLIgnore]
    public uint xmin { get; set; }

    public virtual Card Card { get; set; }
    public virtual User User { get; set; }
  }
}