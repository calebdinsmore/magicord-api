using System.Threading.Tasks;

namespace Magicord.Modules.Scryfall 
{
  public interface IScryfallService
  {
    Task<CardSearchResultDto> SearchCards(string q, int page = 1);
  }
}