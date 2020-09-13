using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Magicord.Models.Enums;

namespace Magicord.Models
{
  public partial class Set : IEntity
  {
    public Set()
    {
      Cards = new HashSet<Card>();
      SetTranslations = new HashSet<SetTranslation>();
      Tokens = new HashSet<Token>();
    }

    public long Id { get; set; }
    public long? BaseSetSize { get; set; }
    public string Block { get; set; }
    public string Booster { get; set; }
    public string Code { get; set; }
    public bool IsFoilOnly { get; set; }
    public bool IsForeignOnly { get; set; }
    public bool IsNonFoilOnly { get; set; }
    public bool IsOnlineOnly { get; set; }
    public bool IsPartialPreview { get; set; }
    public string KeyruneCode { get; set; }
    public long? McmId { get; set; }
    public string McmName { get; set; }
    public string MtgoCode { get; set; }
    public string Name { get; set; }
    public string ParentCode { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public long? TcgplayerGroupId { get; set; }
    public long? TotalSetSize { get; set; }
    public SetsType Type { get; set; }

    public virtual ICollection<Card> Cards { get; set; }
    public virtual ICollection<SetTranslation> SetTranslations { get; set; }
    public virtual ICollection<Token> Tokens { get; set; }
    public virtual ICollection<UserBooster> UserBoosters { get; set; }
    public virtual ICollection<StoreBoosterListing> StoreBoosterListings { get; set; }
  }
}
