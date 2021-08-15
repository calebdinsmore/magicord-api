using System.Collections.Generic;

namespace Magicord.Models
{
  public partial class SealedEvent : IEntity
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal EntryFee { get; set; }
    public bool PacksAreDistributed { get; set; }

    public virtual ICollection<SealedEventAttendee> SealedEventAttendees { get; set; }
    public virtual ICollection<SealedEventPack> SealedEventPacks { get; set; }
  }
}