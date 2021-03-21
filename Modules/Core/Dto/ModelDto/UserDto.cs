using System.Collections.Generic;

namespace Magicord.Modules.Core.Dto
{
  public partial class UserDto
  {
    public long Id { get; set; }
    public decimal Balance { get; set; }

    public virtual ICollection<UserCardDto> UserCards { get; set; }
    public virtual ICollection<UserBoosterDto> UserBoosters { get; set; }
  }
}