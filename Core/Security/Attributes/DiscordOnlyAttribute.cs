using System;
using System.Net;
using Magicord.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Magicord.Core.Security
{
  public class DiscordOnlyAttribute : ActionFilterAttribute
  {
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    private string _discordAccessKey { get; set; }
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      var services = filterContext.HttpContext.RequestServices;
      var configSettings = services.GetService<IOptions<ConfigSettings>>();

      _httpContextAccessor = services.GetService<IHttpContextAccessor>();
      _discordAccessKey = configSettings.Value?.DiscordAccessKey;

      if (string.IsNullOrWhiteSpace(_discordAccessKey))
      {
        throw new ArgumentNullException("DiscordAccessKey");
      }
      if (_httpContextAccessor == null)
      {
        throw new ArgumentNullException("HttpContextAccessor");
      }

      filterContext.HttpContext.Request.Headers.TryGetValue("DiscordAccessKey", out var accessKey);

      if (accessKey != _discordAccessKey)
      {
        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        filterContext.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
      }
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
  }
}