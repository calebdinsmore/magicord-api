using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicord.Modules.Integration.TcgPlayer
{
  public interface ITcgPlayerService
  {
    Task<List<TcgPricePointDto>> GetCardPricePoints(string tcgProductId);
  }
}