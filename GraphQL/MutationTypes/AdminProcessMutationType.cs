using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.AdminProcess;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public async Task<bool> UpdateWithMtgJson([Service] IAdminProcessService service)
    {
      await service.PullDownMtgJsonData();
      return true;
    }

    public async Task<bool> UpdateCardPrices([Service] IAdminProcessService service)
    {
      await service.UpdateCardPrices();
      return true;
    }
  }
}