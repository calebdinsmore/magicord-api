using System.Collections.Generic;

namespace Magicord.Modules.Core.Dto
{
  public class UserBoosterDto
  {
    public long Id { get; set; }
    public long UserId { get; set; }
    public string SetCode { get; set; }
    public decimal BuyPrice { get; set; }
    public bool IsOpened { get; set; }

    public virtual UserDto User { get; set; }
    public virtual SetDto Set { get; set; }
    public virtual ICollection<UserBoosterCardDto> UserBoosterCards { get; set; }
  }
}