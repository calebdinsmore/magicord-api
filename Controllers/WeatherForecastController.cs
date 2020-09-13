using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicord.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Magicord.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly MagicordContext _magicordContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, MagicordContext context)
    {
      _logger = logger;
      _magicordContext = context;
    }

    [HttpGet]
    public Set Get()
    {
      var set = _magicordContext.Sets.First(x => x.Booster != null);
      return set;
    }
  }
}
