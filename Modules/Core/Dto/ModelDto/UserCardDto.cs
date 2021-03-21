using System.Collections.Generic;

namespace Magicord.Modules.Core.Dto
{
  public class UserCardDto
  {
    public long UserId { get; set; }
    public string CardUuid { get; set; }

    public virtual UserDto User { get; set; }
    public virtual CardDto Card { get; set; }
  }
}