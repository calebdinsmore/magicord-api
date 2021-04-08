using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Magicord.Core.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Magicord.Core.ApiKeyAuthorization
{
  public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions { }

  public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>
  {
    public const string API_KEY_HEADER_NAME = "x-magicord-api-key";
    private readonly IOptions<ConfigSettings> _configSettings;

    public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptions<ConfigSettings> configSettings) : base(options, logger, encoder, clock)
    {
      _configSettings = configSettings;
    }
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      if (!Request.Headers.ContainsKey(API_KEY_HEADER_NAME))
      {
        return Task.FromResult(AuthenticateResult.Fail("Required header not found."));
      }

      var discordAccessKey = _configSettings.Value?.DiscordAccessKey;

      var token = Request.Headers[API_KEY_HEADER_NAME].ToString();

      if (token == discordAccessKey)
      {
        var claims = new[] {
          new Claim(ClaimTypes.NameIdentifier, "MagicordBot")
        };

        var claimsIdentity = new ClaimsIdentity(claims, nameof(ApiKeyAuthenticationHandler));

        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
      }

      return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
    }
  }
}