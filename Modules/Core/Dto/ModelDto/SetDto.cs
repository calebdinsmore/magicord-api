using System;
using System.Collections.Generic;
using Magicord.Models.Enums;

namespace Magicord.Modules.Core.Dto
{
  public class SetDto
  {
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

    public virtual ICollection<CardDto> Cards { get; set; }
    public virtual ICollection<UserBoosterDto> UserBoosters { get; set; }
  }
}