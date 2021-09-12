using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate.Execution;
using Magicord.Core.Exceptions;
using Magicord.Models;
using Magicord.Modules.AdminProcess;
using Magicord.Modules.Integration.TcgPlayer;
using Magicord.Modules.Users.Dto;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Users
{
  public class UserService : IUserService
  {
    private readonly MagicordContext _dataContext;
    private readonly IMapper _mapper;
    private readonly ITcgPlayerService _tcgPlayerService;
    private readonly IAdminProcessService _adminProcessService;
    public UserService(MagicordContext dataContext, IMapper mapper, ITcgPlayerService tcgPlayerService, IAdminProcessService adminProcessService)
    {
      _dataContext = dataContext;
      _mapper = mapper;
      _tcgPlayerService = tcgPlayerService;
      _adminProcessService = adminProcessService;
    }

    public IEnumerable<UserDto> GetAll()
    {
      var result = _mapper.Map<IEnumerable<UserDto>>(_dataContext.Users.AsEnumerable());
      return result;
    }

    public UserDto Create(UserDto dto)
    {
      var entity = _mapper.Map<User>(dto);
      entity.Balance = 50;
      if (_dataContext.Users.Any(x => x.Id == dto.Id))
      {
        throw new InvalidMagicordOperationException("A user with this Discord ID already exists.");
      }
      _dataContext.Users.Add(entity);
      _dataContext.SaveChanges();

      return _mapper.Map<UserDto>(entity);
    }

    public long Upsert(UserDto dto)
    {
      var entity = _mapper.Map<User>(dto);
      if (_dataContext.Users.Find(dto.Id) != null)
      {
        this._dataContext.Users.Update(entity);
      }
      else
      {
        this._dataContext.Users.Add(entity);
      }

      _dataContext.SaveChanges();
      return entity.Id;
    }

    public User CreateUser(CreateUserInputDto dto)
    {
      if (GetUserById(dto.Id).Any())
      {
        throw new QueryException("User already exists.");
      }
      var newUser = _mapper.Map<User>(dto);
      newUser.Balance = 50;
      _dataContext.Add(newUser);
      _dataContext.SaveChanges();
      return newUser;
    }

    public IQueryable<User> GetUsers()
    {
      return _dataContext.Users;
    }

    public IQueryable<User> GetUserById(long id)
    {
      return _dataContext.Users.Where(x => x.Id == id);
    }

    public User AddToUserBalance(AddToUserBalanceInputDto dto)
    {
      var user = _dataContext.Users.Find(dto.Id);
      if (user == null)
      {
        throw new QueryException("User doesn't exist.");
      }
      user.Balance += dto.Balance;
      _dataContext.Update(user);
      _dataContext.SaveChanges();
      return user;
    }

    public UserStatsDto GetUserStats(long id)
    {
      var user = _dataContext.Users
        .AsSplitQuery()
        .Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice)
        .Include(x => x.UserShares).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice)
        .Include(x => x.UserShorts).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice)
        .Where(x => x.Id == id)
        .FirstOrDefault();
      return new UserStatsDto
      {
        Balance = user.Balance,
        BuylistValue = user.UserCards.Sum(x => (x.Card.CardPrice.CurrentBuylistNonFoil * x.AmountNonFoil) + (x.Card.CardPrice.CurrentBuylistFoil * x.AmountFoil)),
        LongPositionValue = user.UserShares.Sum(x => x.CurrentValue * x.Amount),
        ShortPositionValue = user.UserShorts.Sum(x => x.ReservedCash - (x.BuybackCost * x.Amount)),
        NumberOfCardsOwned = user.UserCards.Sum(x => x.AmountFoil + x.AmountNonFoil)
      };
    }

    public List<UserCollectionValueChangeDto> GetChangeInUserCollectionValues()
    {
      var collectionList = new List<UserCollectionValueChangeDto>();
      var users = _dataContext.Users
        .Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice)
        .Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPriceHistories.OrderByDescending(cph => cph.DateRecorded).Take(1));
      foreach (var user in users)
      {
        collectionList.Add(new UserCollectionValueChangeDto
        {
          UserId = user.Id,
          NewCollectionValue = user.UserCards.Sum(x => (x.Card.CardPrice.CurrentBuylistNonFoil * x.AmountNonFoil) + (x.Card.CardPrice.CurrentBuylistFoil * x.AmountFoil)),
          OldCollectionValue = user.UserCards.Sum(x => (x.Card.CardPriceHistories.First().BuylistNonFoil * x.AmountNonFoil) + (x.Card.CardPriceHistories.First().BuylistFoil * x.AmountFoil))
        });
      }
      return collectionList;
    }

    public IQueryable<UserCard> GetUserCardsById(long id)
    {
      return _dataContext.Users
        .Include(x => x.UserCards)
        .ThenInclude(x => x.Card)
        .FirstOrDefault(x => x.Id == id)
        ?.UserCards.AsQueryable();
    }

    public async Task<StockTransactionResultDto> BuySharesAsync(StockTransactionInputDto input)
    {
      input.OrderType = OrderTypeEnum.Buy;
      input.Validate(_dataContext);
      var existingShares = _dataContext.UserShares.FirstOrDefault(x => x.UserId == input.UserId && x.CardId == input.CardId && x.IsFoil == input.IsFoil);
      var user = _dataContext.Users.FirstOrDefault(x => x.Id == input.UserId);
      var card = _dataContext.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Id == input.CardId);
      await HandleChangedPriceAsync(card, input.IsFoil);
      var cardShareValue = input.IsFoil ? card.CardPrice.CurrentRetailFoil : card.CardPrice.CurrentRetailNonFoil;
      var orderValue = input.DollarAmount ?? 0;
      if (orderValue == 0)
      {
        orderValue = (input.ShareAmount ?? 0) * cardShareValue;
      }
      var shareAmount = input.ShareAmount ?? 0;
      if (shareAmount == 0)
      {
        shareAmount = (input.DollarAmount ?? 0) / cardShareValue;
      }

      user.Balance -= orderValue;
      if (existingShares == null)
      {
        existingShares = new UserShare
        {
          CardId = input.CardId,
          UserId = input.UserId,
          Amount = shareAmount,
          CashInvested = orderValue,
          IsFoil = input.IsFoil,
          AverageInvestedValue = cardShareValue
        };
        _dataContext.Add(existingShares);
      }
      else
      {
        existingShares.CashInvested += orderValue;
        existingShares.AverageInvestedValue = ((cardShareValue * shareAmount) + (existingShares.AverageInvestedValue * existingShares.Amount)) / (existingShares.Amount + shareAmount);
        existingShares.Amount += shareAmount;
      }
      _dataContext.SaveChanges();
      return new StockTransactionResultDto
      {
        DollarAmount = orderValue,
        CurrentShare = existingShares
      };
    }

    public async Task<StockTransactionResultDto> SellSharesAsync(StockTransactionInputDto input)
    {
      input.OrderType = OrderTypeEnum.Sell;
      input.Validate(_dataContext);
      var existingShares = _dataContext.UserShares.FirstOrDefault(x => x.UserId == input.UserId && x.CardId == input.CardId && x.IsFoil == input.IsFoil);
      var user = _dataContext.Users.FirstOrDefault(x => x.Id == input.UserId);
      var card = _dataContext.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Id == input.CardId);
      await HandleChangedPriceAsync(card, input.IsFoil);
      var cardShareValue = input.IsFoil ? card.CardPrice.CurrentRetailFoil : card.CardPrice.CurrentRetailNonFoil;
      var orderValue = input.DollarAmount ?? 0;
      if (orderValue == 0)
      {
        orderValue = (input.ShareAmount ?? 0) * cardShareValue;
      }
      var shareAmount = input.ShareAmount ?? 0;
      if (shareAmount == 0)
      {
        shareAmount = (input.DollarAmount ?? 0) / cardShareValue;
      }

      user.Balance += orderValue;
      existingShares.Amount -= shareAmount;
      existingShares.CashInvested -= orderValue;
      if (existingShares.CashInvested < 0)
      {
        existingShares.CashInvested = 0;
      }
      if (existingShares.Amount == 0)
      {
        _dataContext.Remove(existingShares);
      }
      _dataContext.SaveChanges();
      return new StockTransactionResultDto
      {
        DollarAmount = orderValue,
        CurrentShare = existingShares
      };
    }

    public async Task<StockTransactionResultDto> ShortSharesAsync(StockTransactionInputDto input)
    {
      input.OrderType = OrderTypeEnum.Short;
      input.Validate(_dataContext);
      var existingShorts = _dataContext.UserShorts.FirstOrDefault(x => x.UserId == input.UserId && x.CardId == input.CardId && x.IsFoil == input.IsFoil);
      var user = _dataContext.Users.FirstOrDefault(x => x.Id == input.UserId);
      var card = _dataContext.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Id == input.CardId);
      await HandleChangedPriceAsync(card, input.IsFoil);
      var cardShareValue = input.IsFoil ? card.CardPrice.CurrentRetailFoil : card.CardPrice.CurrentRetailNonFoil;
      var orderValue = input.DollarAmount ?? 0;
      if (orderValue == 0)
      {
        orderValue = (input.ShareAmount ?? 0) * cardShareValue;
      }
      var shareAmount = input.ShareAmount ?? 0;
      if (shareAmount == 0)
      {
        shareAmount = (input.DollarAmount ?? 0) / cardShareValue;
      }

      user.Balance -= orderValue;
      if (existingShorts == null)
      {
        existingShorts = new UserShort
        {
          CardId = input.CardId,
          UserId = input.UserId,
          Amount = shareAmount,
          ReservedCash = orderValue * 2,
          IsFoil = input.IsFoil,
          ShortedValue = cardShareValue
        };
        _dataContext.Add(existingShorts);
      }
      else
      {
        existingShorts.ReservedCash += orderValue * 2;
        existingShorts.ShortedValue = ((cardShareValue * shareAmount) + (existingShorts.ShortedValue * existingShorts.Amount)) / (existingShorts.Amount + shareAmount);
        existingShorts.Amount += shareAmount;
      }
      _dataContext.SaveChanges();
      return new StockTransactionResultDto
      {
        DollarAmount = orderValue,
        CurrentShort = existingShorts
      };
    }

    public StockTransactionResultDto AddToShortCashReserve(ShortAdjustmentInputDto input)
    {
      input.Validate(_dataContext);
      var dollarAmount = input.DollarAmount ?? 0;
      var userShort = _dataContext.UserShorts.FirstOrDefault(x => x.CardId == input.CardId && x.IsFoil == input.IsFoil && x.UserId == input.UserId);
      var user = _dataContext.Users.Find(input.UserId);
      if (userShort == null)
      {
        throw new QueryException("Unable to find a short position with that ID.");
      }
      if (user == null)
      {
        throw new QueryException("Unable to find a user with that ID. Try `mc start`?");
      }
      if ((input.DollarAmount ?? 0) > user.Balance)
      {
        throw new QueryException("Insufficient funds to add specified amount to short position reserves.");
      }
      user.Balance -= dollarAmount;
      userShort.ReservedCash += dollarAmount;
      _dataContext.SaveChanges();
      return new StockTransactionResultDto
      {
        DollarAmount = dollarAmount,
        CurrentShort = userShort
      };
    }

    public async Task<StockTransactionResultDto> ReduceShortPositionAsync(ShortAdjustmentInputDto input)
    {
      input.Validate(_dataContext);
      var user = _dataContext.Users.FirstOrDefault(x => x.Id == input.UserId);
      var userShort = _dataContext.UserShorts.FirstOrDefault(x => x.CardId == input.CardId && x.IsFoil == input.IsFoil && x.UserId == input.UserId);
      if (userShort.IsRed)
      {
        throw new QueryException("You can't reduce a short position that's in the red. Either close the position with `mc stocks close_short` or add to its reserves with `mc stocks bolster_short`.");
      }
      var card = _dataContext.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Id == input.CardId);
      await HandleChangedPriceAsync(card, input.IsFoil);
      var cardShareValue = input.IsFoil ? card.CardPrice.CurrentRetailFoil : card.CardPrice.CurrentRetailNonFoil;
      var orderValue = input.DollarAmount ?? 0;
      if (orderValue == 0)
      {
        orderValue = (input.ShareAmount ?? 0) * cardShareValue;
      }
      var shareAmount = input.ShareAmount ?? 0;
      if (shareAmount == 0)
      {
        shareAmount = (input.DollarAmount ?? 0) / cardShareValue;
      }

      userShort.ReservedCash -= orderValue * 2;
      user.Balance += orderValue;
      userShort.Amount -= shareAmount;
      if (userShort.Amount == 0)
      {
        _dataContext.Remove(userShort);
        if (userShort.ReservedCash > 0)
        {
          orderValue += userShort.ReservedCash;
          user.Balance += userShort.ReservedCash;
        }
      }
      _dataContext.SaveChanges();
      return new StockTransactionResultDto
      {
        DollarAmount = orderValue,
        CurrentShort = userShort
      };
    }

    public async Task<StockTransactionResultDto> CloseShortPositionAsync(ShortAdjustmentInputDto input)
    {
      var userShort = _dataContext.UserShorts.FirstOrDefault(x => x.CardId == input.CardId && x.IsFoil == input.IsFoil && x.UserId == input.UserId);
      if (userShort == null)
      {
        input.Validate(_dataContext);
      }
      if (!userShort.IsRed)
      {
        input.ShareAmount = userShort.Amount;
        return await ReduceShortPositionAsync(input);
      }
      _dataContext.Remove(userShort);
      _dataContext.SaveChanges();
      return new StockTransactionResultDto
      {
        DollarAmount = 0,
        CurrentShort = null
      };
    }

    public IQueryable<UserShare> GetUserSharesById(long id)
    {
      return _dataContext.UserShares.Where(x => x.UserId == id);
    }

    public IQueryable<UserShort> GetUserShortsById(long id)
    {
      return _dataContext.UserShorts.Where(x => x.UserId == id);
    }

    private async Task HandleChangedPriceAsync(Card card, bool isFoil)
    {
      var changedPrice = await CheckForPriceChangeAsync(card, isFoil);
      if (changedPrice != 0)
      {
        throw new QueryException($"Market price for {card.Name} has changed to ${changedPrice}.\nIf you still want to place your order, rerun your command.");
      }
    }

    private async Task<decimal> CheckForPriceChangeAsync(Card card, bool isFoil)
    {
      try
      {
        if (card.TcgplayerProductId == null)
        {
          return 0;
        }
        var currentMarketPrice = isFoil ? card.CardPrice.CurrentRetailFoil : card.CardPrice.CurrentRetailNonFoil;
        var pricePoints = await _tcgPlayerService.GetCardPricePoints(card.TcgplayerProductId);
        var pricePoint = isFoil ? pricePoints.FirstOrDefault(x => x.PrintingType == PrintingTypeEnum.Foil) : pricePoints.FirstOrDefault(x => x.PrintingType == PrintingTypeEnum.Normal);
        var marketPrice = pricePoint?.MarketPrice ?? 0;
        if (marketPrice == 0)
        {
          return 0;
        }
        if (pricePoint.MarketPrice != currentMarketPrice)
        {
          _adminProcessService.ArchiveCardPrice(card.CardPrice);
          if (isFoil)
          {
            card.CardPrice.CurrentRetailFoil = marketPrice;
          }
          else
          {
            card.CardPrice.CurrentRetailNonFoil = marketPrice;
          }
          _dataContext.SaveChanges();
          return marketPrice;
        }
        return 0;
      }
      catch (System.Exception e)
      {
        Console.WriteLine($"Encountered an error fetching latest prices for {card.Id}.\n{e.ToString()}");

        return 0;
      }
    }
  }
}