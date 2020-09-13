using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Magicord.Models
{
  public class StoreBoosterListing : IEntity
  {
    public long Id { get; set; }

    [Required]
    public string SetCode { get; set; }

    [DefaultValue("default")]
    public string BoosterType { get; set; }

    [Required]
    public decimal RetailPrice { get; set; }

    public bool IsActive { get; set; }

    public Set Set { get; set; }
  }
}
