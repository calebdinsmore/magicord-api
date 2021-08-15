using Magicord.Models;

namespace Magicord.Modules.Core
{
  public interface IValidatable
  {
    void Validate(MagicordContext context);
  }
}