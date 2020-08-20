namespace Magicord.Models
{
  public partial class User : IEntity
  {
    public long Id { get; set; }
    public decimal Balance { get; set; }
  }
}