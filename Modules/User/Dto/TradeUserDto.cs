using System.Collections.Generic;
using Magicord.Models;

namespace Magicord.Modules.Users
{
  public class TradeUserDto
  {
    public long UserId { get; set; }
    public List<UserCard> UserCardOffers { get; set; }
    public decimal CashOffer { get; set; }
  }
}