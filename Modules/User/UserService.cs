using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate.Execution;
using Magicord.Core.Exceptions;
using Magicord.Models;
using Magicord.Modules.Users.Dto;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Users
{
  public class UserService : IUserService
  {
    private readonly MagicordContext _dataContext;
    private readonly IMapper _mapper;
    public UserService(MagicordContext dataContext, IMapper mapper)
    {
      _dataContext = dataContext;
      _mapper = mapper;
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
      var user = _dataContext.Users.Include(x => x.UserCards).ThenInclude(x => x.Card).ThenInclude(x => x.CardPrice).Where(x => x.Id == id).FirstOrDefault();
      return new UserStatsDto
      {
        Balance = user.Balance,
        NetWorth = user.Balance + user.UserCards.Sum(x => (x.Card.CardPrice.CurrentBuylistNonFoil * x.AmountNonFoil) + (x.Card.CardPrice.CurrentBuylistFoil * x.AmountFoil)),
        NumberOfCardsOwned = user.UserCards.Sum(x => x.AmountFoil + x.AmountNonFoil)
      };
    }

    public IQueryable<UserCard> GetUserCardsById(long id)
    {
      return _dataContext.Users
        .Include(x => x.UserCards)
        .ThenInclude(x => x.Card)
        .FirstOrDefault(x => x.Id == id)
        ?.UserCards.AsQueryable();
    }
  }
}