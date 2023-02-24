using System.Net.Http;
using System.Threading.Tasks;
using Magicord.Modules.Core.Extension;

namespace Magicord.Modules.Scryfall
{
  public class ScryfallService : IScryfallService
  {
    static readonly HttpClient client = new HttpClient();
    static readonly string baseUrl = "https://api.scryfall.com";

    public async Task<CardSearchResultDto> SearchCards(string q, int page = 1)
    {
      var response = await client.GetAsync($"{baseUrl}/cards/search?q={q}&page={page}");
      response.EnsureSuccessStatusCode();
      var jsonString = await response.Content.ReadAsStringAsync();
      return jsonString.ParseJson<CardSearchResultDto>();
    }
  }
}