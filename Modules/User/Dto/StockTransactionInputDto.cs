using System;
using System.Linq;
using HotChocolate.Execution;
using Magicord.Models;
using Magicord.Modules.Core;
using Microsoft.EntityFrameworkCore;

namespace Magicord.Modules.Users
{
  public enum OrderTypeEnum
  {
    Buy,
    Sell,
    Short
  }

  public class StockTransactionInputDto : IValidatable
  {
    public OrderTypeEnum? OrderType { get; set; }
    public decimal? ShareAmount { get; set; }
    public decimal? DollarAmount { get; set; }
    public long CardId { get; set; }
    public long UserId { get; set; }
    public bool IsFoil { get; set; }

    public void Validate(MagicordContext context)
    {
      ShareAmount = ShareAmount ?? 0;
      DollarAmount = DollarAmount ?? 0;
      if (ShareAmount > 0 && DollarAmount > 0)
      {
        throw new QueryException("You cannot supply both a share amount and dollar amount. Must be either/or.");
      }

      if (DollarAmount < 0 || ShareAmount < 0)
      {
        throw new QueryException("Dollar amount and share amount must be non-negative");
      }

      if (DollarAmount == 0 && ShareAmount == 0)
      {
        throw new QueryException("Must specify either a share amount or dollar amount.");
      }

      var user = context.Users.FirstOrDefault(x => x.Id == UserId);
      var card = context.Cards.Include(x => x.CardPrice).FirstOrDefault(x => x.Id == CardId);
      if (user == null || card == null)
      {
        throw new QueryException("Unable to locate either the user or the card ID.");
      }

      var userIsFrozen = context.UserShorts.Include(x => x.Card).ThenInclude(x => x.CardPrice)
        .Where(x => (x.IsFoil ? x.Card.CardPrice.CurrentRetailFoil : x.Card.CardPrice.CurrentRetailNonFoil) * x.Amount > x.ReservedCash).Any();

      if (userIsFrozen && OrderType != OrderTypeEnum.Sell)
      {
        throw new QueryException("Your account is frozen due to short positions that are currently in the red. Check your portfolio with `mc portfolio`.");
      }

      var currentValue = IsFoil ? card.CardPrice.CurrentRetailFoil : card.CardPrice.CurrentRetailNonFoil;

      if (currentValue == 0)
      {
        throw new QueryException("You cannot purchase, sell, or short shares of a card whose retail value is 0.");
      }

      if (OrderType == OrderTypeEnum.Sell)
      {
        var existingShares = context.UserShares.FirstOrDefault(x => x.UserId == UserId && x.CardId == card.Id && x.IsFoil == IsFoil);

        if (existingShares == null)
        {
          throw new QueryException("You do not own shares of this card.");
        }

        if (Math.Round(ShareAmount ?? 0, 2) == Math.Round(existingShares.Amount, 2))
        {
          ShareAmount = existingShares.Amount;
        }

        if (ShareAmount > existingShares.Amount)
        {
          throw new QueryException("You cannot sell an amount of shares greater than the amount you own.");
        }

        var currentShareValue = existingShares.Amount * currentValue;

        if (Math.Round(DollarAmount ?? 0, 2) == Math.Round(currentShareValue, 2))
        {
          DollarAmount = currentShareValue;
        }

        if (DollarAmount > currentShareValue)
        {
          throw new QueryException("You cannot sell for a dollar amount greater than the current value of your shares.");
        }
      }
      else
      {
        if (ShareAmount * currentValue > user.Balance)
        {
          throw new QueryException("Insufficient funds to place order.");
        }

        if (DollarAmount > user.Balance)
        {
          throw new QueryException("Insufficient funds to place order.");
        }
      }
    }
  }
}