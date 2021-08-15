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
      return _dataContext.SealedEvents.Where(x => !x.PacksAreDistributed);
    }

    public List<UserPacks> DistributeUserPacks(long sealedEventId)
    {
      var sealedEvent = _dataContext.SealedEvents
        .Include(x => x.SealedEventAttendees)
        .ThenInclude(x => x.User)
        .Include(x => x.SealedEventPacks)
        .FirstOrDefault(x => x.Id == sealedEventId);

      if (sealedEvent == null)
      {
        throw new QueryException($"Could not find sealed event with ID {sealedEventId}");
      }

      if (sealedEvent.PacksAreDistributed)
      {
        throw new QueryException("The packs in this sealed event have already been distributed.");
      }

      var allUserPacks = new List<UserPacks>();
      foreach (var attendee in sealedEvent.SealedEventAttendees)
      {
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
      }
      sealedEvent.PacksAreDistributed = true;
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
      newEvent.PacksAreDistributed = false;
      _dataContext.Add(newEvent);
      _dataContext.SaveChanges();
      return newEvent;
    }
  }
}