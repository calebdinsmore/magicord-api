using System.Threading.Tasks;

namespace Magicord.Modules.AdminProcess
{
  public interface IAdminProcessService
  {
    Task PullDownMtgJsonData();
  }
}