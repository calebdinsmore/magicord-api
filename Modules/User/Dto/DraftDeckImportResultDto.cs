using System.Collections.Generic;
using Magicord.Models;

namespace Magicord.Modules.Users
{
  public class DraftDeckImportResultDto
  {
    public DraftDeckImportResultDto()
    {
      Errors = new List<string>();
      DeckCards = new List<DraftCardResultDto>();
      SideboardCards = new List<DraftCardResultDto>();
    }
    public bool HasErrors 
    {
      get 
      {
        return Errors.Count > 0;
      }
    }

    public List<string> Errors { get; set; }

    public List<DraftCardResultDto> DeckCards { get; set; }
    public List<DraftCardResultDto> SideboardCards { get; set; }
  }
}

