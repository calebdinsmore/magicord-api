namespace Magicord.Modules.Users
{
  public class UserCollectionValueChangeDto
  {
    public long UserId { get; set; }
    public decimal OldCollectionValue { get; set; }
    public decimal NewCollectionValue { get; set; }
  }
}