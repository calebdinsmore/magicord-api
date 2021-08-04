using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Magicord.Models;
using Magicord.Modules.Core.Extension;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.AdminProcess
{
  public class AdminProcessService : IAdminProcessService
  {
    static readonly HttpClient client = new HttpClient();

    private MagicordContext _dataContext;
    private IMapper _mapper;

    public AdminProcessService(MagicordContext dataContext, IMapper mapper)
    {
      _dataContext = dataContext;
      _mapper = mapper;
    }
    public async Task PullDownMtgJsonData()
    {
      try
      {
        var allPrintings = await GetAllPrintingsJson();
        var sets = GetNewSets(allPrintings);
        PersistSets(sets);
        var allPrices = await GetAllPricesJson();
        PersistCardPrices(allPrices);
        CreateBlankCardPrices();
      }
      catch (System.Exception)
      {

        throw;
      }
    }

    private void CreateBlankCardPrices()
    {
      var cardsWithoutPrices = _dataContext.Cards.Include(x => x.CardPrice).Where(x => x.CardPrice == null);
      foreach (var card in cardsWithoutPrices)
      {
        _dataContext.Add(new CardPrice
        {
          CardUuid = card.Uuid,
          CurrentBuylistFoil = 0,
          CurrentBuylistNonFoil = 0,
          CurrentRetailFoil = 0,
          CurrentRetailNonFoil = 0
        });
      }
      _dataContext.SaveChanges();
    }

    private int PersistCardPrices(AllPricesJson allPricesJson)
    {
      var cardUuidDict = new Dictionary<string, bool>();
      foreach (var uuid in _dataContext.Cards.Select(x => x.Uuid).ToList())
      {
        cardUuidDict.Add(uuid, true);
      }
      foreach (var cardUuid in allPricesJson.Data.Keys)
      {
        if (!cardUuidDict.ContainsKey(cardUuid))
        {
          continue;
        }
        var priceData = allPricesJson.Data.GetValueOrDefault(cardUuid);
        var buylist = priceData.Paper?.CardKingdom?.Buylist;
        var retail = priceData.Paper?.CardKingdom?.Retail;
        var buylistFoil = buylist?.Foil?.OrderByDescending(x => x.Key)?.FirstOrDefault().Value ?? 0;
        var buylistNonFoil = buylist?.Normal?.OrderByDescending(x => x.Key)?.FirstOrDefault().Value ?? 0;
        var retailFoil = retail?.Foil?.OrderByDescending(x => x.Key)?.FirstOrDefault().Value ?? 0;
        var retailNonFoil = retail?.Normal?.OrderByDescending(x => x.Key)?.FirstOrDefault().Value ?? 0;
        var existing = _dataContext.CardPrices.FirstOrDefault(x => x.CardUuid == cardUuid);
        if (existing != null)
        {
          existing.CurrentBuylistFoil = buylistFoil;
          existing.CurrentBuylistNonFoil = buylistNonFoil;
          existing.CurrentRetailFoil = retailFoil;
          existing.CurrentRetailNonFoil = retailNonFoil;
        }
        else
        {
          var newCardPrice = new CardPrice
          {
            CardUuid = cardUuid,
            CurrentBuylistFoil = buylistFoil,
            CurrentBuylistNonFoil = buylistNonFoil,
            CurrentRetailFoil = retailFoil,
            CurrentRetailNonFoil = retailNonFoil
          };
          _dataContext.Add(newCardPrice);
        }
      }
      return _dataContext.SaveChanges();
    }

    private void PersistSets(IEnumerable<Set> newSets)
    {
      foreach (var set in newSets)
      {
        _dataContext.Add(set);
      }
      _dataContext.SaveChanges();
    }

    private IEnumerable<Set> GetNewSets(AllPrintingsJson allPrintingsJson)
    {
      var newSets = new List<Set>();
      foreach (var setCode in allPrintingsJson.Data.Keys)
      {
        if (_dataContext.Sets.FirstOrDefault(x => x.Code == setCode) == null)
        {
          var setJson = allPrintingsJson.Data.GetValueOrDefault(setCode);
          setJson.Type = setJson.Type.Replace("_", string.Empty);
          var set = _mapper.Map<Set>(allPrintingsJson.Data.GetValueOrDefault(setCode));
          newSets.Add(set);
        }
      }
      return newSets.AsEnumerable();
    }

    private async Task<AllPrintingsJson> GetAllPrintingsJson()
    {
      var response = await client.GetAsync("https://mtgjson.com/api/v5/AllPrintings.json");
      response.EnsureSuccessStatusCode();
      var jsonString = await response.Content.ReadAsStringAsync();
      return jsonString.ParseJson<AllPrintingsJson>();
    }

    private async Task<AllPricesJson> GetAllPricesJson()
    {
      var response = await client.GetAsync("https://mtgjson.com/api/v5/AllPrices.json");
      response.EnsureSuccessStatusCode();
      var jsonString = await response.Content.ReadAsStringAsync();
      return jsonString.ParseJson<AllPricesJson>();
    }
  }
}