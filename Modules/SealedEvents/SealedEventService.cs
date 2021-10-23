using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.Booster;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.SealedEvents
{
  public class SealedEventService : ISealedEventService
  {
    private readonly MagicordContext _dataContext;
    private readonly IBoosterService _boosterService;
    private readonly IMapper _mapper;
    public SealedEventService(MagicordContext dataContext, IMapper mapper, IBoosterService boosterService)
    {
      _dataContext = dataContext;
      _mapper = mapper;
      _boosterService = boosterService;
    }

    public IQueryable<SealedEvent> GetActiveSealedEvents()
    {
      return _dataContext.SealedEvents.Where(x => x.IsActive);
    }

    public List<UserPacks> DistributeUserPacks(long sealedEventId)
    {
      var sealedEvent = _dataContext.SealedEvents
        .Include(x => x.SealedEventAttendees)
        .ThenInclude(x => x.User)
        .ThenInclude(x => x.UserCards)
        .Include(x => x.SealedEventPacks)
        .Include(x => x.SealedEventPromos)
        .FirstOrDefault(x => x.Id == sealedEventId);

      if (sealedEvent == null)
      {
        throw new QueryException($"Could not find sealed event with ID {sealedEventId}");
      }

      var allUserPacks = new List<UserPacks>();
      foreach (var attendee in sealedEvent.SealedEventAttendees)
      {
        if (attendee.HasBeenGivenPacks)
        {
          continue;
        }
        foreach (var sealedEventPack in sealedEvent.SealedEventPacks)
        {
          var packs = _boosterService.GenerateMultipleBoosters(sealedEventPack.SetCode, sealedEventPack.PackCount);
          allUserPacks.Add(new UserPacks
          {
            UserId = attendee.UserId,
            Packs = packs
          });
          foreach (var pack in packs)
          {
            _boosterService.AddBoosterCardsToUser(pack.Cards, attendee.User);
          }
        }
        foreach (var promo in sealedEvent.SealedEventPromos)
        {
          var promoCard = GetPromoCard(promo);
          var boosterCards = new List<BoosterCardDto>
          {
            new BoosterCardDto { Card = promoCard, Foil = promoCard.HasFoil }
          };
          _boosterService.AddBoosterCardsToUser(boosterCards, attendee.User);
          allUserPacks.Add(new UserPacks
          {
            UserId = attendee.UserId,
            PromoCard = promoCard
          });

        }
        attendee.HasBeenGivenPacks = true;
      }
      _dataContext.SaveChanges();
      return allUserPacks;
    }

    public SealedEventPack AddPackToSealedEvent(AddPackToSealedInputDto input)
    {
      input.Validate(_dataContext);
      var newPack = _mapper.Map<SealedEventPack>(input);
      _dataContext.Add(newPack);
      _dataContext.SaveChanges();
      return newPack;
    }

    public SealedEventPromo AddPromoToSealedEvent(AddPromoToSealedInputDto input)
    {
      input.Validate(_dataContext);
      var promo = _mapper.Map<SealedEventPromo>(input);
      _dataContext.Add(promo);
      _dataContext.SaveChanges();
      return promo;
    }

    public SealedEventAttendee AddSealedEventAttendee(AddSealedEventAttendeeInputDto input)
    {
      input.Validate(_dataContext);
      if (input.ProcessFee)
      {
        var user = _dataContext.Users.First(x => x.Id == input.UserId);
        var sealedEvent = _dataContext.SealedEvents.First(x => x.Id == input.SealedEventId);
        user.Balance -= sealedEvent.EntryFee;
      }
      var attendee = _mapper.Map<SealedEventAttendee>(input);
      _dataContext.Add(attendee);
      _dataContext.SaveChanges();
      return attendee;
    }

    public SealedEvent CreateSealedEvent(CreateSealedEventInputDto input)
    {
      var newEvent = _mapper.Map<SealedEvent>(input);
      newEvent.IsActive = true;
      newEvent.PacksAreDistributed = false;
      _dataContext.Add(newEvent);
      _dataContext.SaveChanges();
      return newEvent;
    }

    private Card GetPromoCard(SealedEventPromo promo)
    {
      var card = _dataContext.Cards
      .Where(
        x => (x.Rarity.ToString().ToLower() == "mythic" || x.Rarity.ToString().ToLower() == "rare")
        && (promo.PromoType != null ? EF.Functions.ILike(x.PromoTypes, $"%{promo.PromoType}%") : x.PromoTypes == null)
        && x.SetCode.ToLower() == promo.SetCode.ToLower()
        && (x.Side == null || x.Side == "a")
      ).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
      if (card == null)
      {
        throw new QueryException("Unable to select a promo card.");
      }
      return card;
    }
  }
}