using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Magicord.Core.ApiKeyAuthorization
{
  public class ApiKeyRequirement : IAuthorizationRequirement
  {
    public IReadOnlyList<string> ApiKeys { get; set; }

    public ApiKeyRequirement(IEnumerable<string> apiKeys)
    {
      ApiKeys = apiKeys?.ToList() ?? new List<string>();
    }
  }
}