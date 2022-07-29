using System.Threading.Tasks;
using Magicord.Models;

namespace Magicord.Modules.AdminProcess
{
  public interface IAdminProcessService
  {
    Task UpdateTokenData();
    Task FixTokenReverseRelated();
    Task PullDownMtgJsonData();
    Task UpdateCardPrices(string dummyArg);
    void ArchiveCardPrice(CardPrice cardPrice);
  }
}