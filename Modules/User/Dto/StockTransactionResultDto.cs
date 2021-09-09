using Magicord.Models;

namespace Magicord.Modules.Users
{
  public class StockTransactionResultDto
  {
    public decimal DollarAmount { get; set; }
    public UserShare CurrentShare { get; set; }
    public UserShort CurrentShort { get; set; }
  }
}