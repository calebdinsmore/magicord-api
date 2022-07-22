using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;

namespace Magicord.Modules.Users
{
  public class DraftDeckImportInputDto
  {
    public long UserId { get; set; }

    [GraphQLType(typeof(AnyType))]
    public Dictionary<string, DraftDeckImportCardMeta> DeckCardUuids { get; set; }
    
    [GraphQLType(typeof(AnyType))]
    public Dictionary<string, DraftDeckImportCardMeta> SideboardCardUuids { get; set; }
  }

  public class DraftDeckImportCardMeta
  {
    public int AmountFoil { get; set; }
    public int AmountNonFoil { get; set; }
  }
}

