using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Magicord.Modules.Core.Extension;

namespace Magicord.Modules.Integration.TcgPlayer
{
  public class TcgPlayerService : ITcgPlayerService
  {
    static readonly HttpClient client = new HttpClient();
    public async Task<List<TcgPricePointDto>> GetCardPricePoints(string tcgProductId)
    {
      var response = await client.GetAsync($"https://mpapi.tcgplayer.com/v2/product/{tcgProductId}/pricepoints");
      response.EnsureSuccessStatusCode();
      var jsonString = await response.Content.ReadAsStringAsync();
      return jsonString.ParseJson<List<TcgPricePointDto>>();
    }
  }
}