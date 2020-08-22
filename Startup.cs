using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Magicord.Core.Configuration;
using Magicord.Core.Middleware;
using Magicord.Core.Security;
using Magicord.Models;
using Magicord.Modules.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Magicord
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAutoMapper(typeof(Startup));
      services.AddControllers().AddNewtonsoftJson();
      services.AddHttpContextAccessor();

      // Configure settins and DbContext
      IConfigurationSection configurationSection = Configuration.GetSection("ConfigSettings");
      services.Configure<ConfigSettings>(configurationSection);
      services.AddDbContext<MagicordContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

      services.AddScoped<IUserManager, UserManager>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware(typeof(HttpErrorHandlingMiddleware));

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
