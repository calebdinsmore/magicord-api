namespace Magicord.Modules.Core.Dto
{
  public class UserBoosterCardDto
  {
    public long Id { get; set; }
    public long UserBoosterId { get; set; }
    public string CardUuid { get; set; }

    public virtual CardDto Card { get; set; }
    public virtual UserBoosterDto UserBooster { get; set; }
  }
}