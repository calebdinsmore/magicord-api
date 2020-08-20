using System;
using System.Net;
using System.Threading.Tasks;
using Magicord.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Magicord.Core.Middleware
{
  public class HttpErrorHandlingMiddleware
  {
    private readonly RequestDelegate next;

    public HttpErrorHandlingMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);
      }
      catch (Exception ex)
      {
        await HandleException(context, ex);
      }
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
      HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

      // Specify different custom exceptions here
      if (ex is ValidationFailureException) code = HttpStatusCode.BadRequest;

      string result = JsonConvert.SerializeObject(new { error = ex.Message });

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)code;

      return context.Response.WriteAsync(result);
    }
  }
}