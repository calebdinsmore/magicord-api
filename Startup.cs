using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Magicord.Core.Configuration;
using Magicord.Core.Middleware;
using Magicord.Core.Security;
using Magicord.GraphQL;
using Magicord.GraphQL.MutationTypes;
using Magicord.GraphQL.QueryTypes;
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
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddGraphQLServer()
        .AddQueryType<UserQueryType>()
        .AddMutationType<UserMutationType>()
        .AddProjections();

      services.AddCors(options =>
      {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                            builder.WithOrigins("http://localhost");
                          });
      });
      services.AddAutoMapper(typeof(Startup));
      services.AddControllers().AddNewtonsoftJson();
      services.AddHttpContextAccessor();


      // Configure settings and DbContext
      IConfigurationSection configurationSection = Configuration.GetSection("ConfigSettings");
      services.Configure<ConfigSettings>(configurationSection);
      services.AddDbContext<MagicordContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

      services.AddScoped<IUserService, UserService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware(typeof(HttpErrorHandlingMiddleware));

      // app.UseHttpsRedirection();


      app.UseCors(MyAllowSpecificOrigins);

      app
        .UseRouting()
        .UseEndpoints(endpoints =>
        {
          endpoints.MapGraphQL();
        });
      // app.UseAuthorization();

      // app.UseEndpoints(endpoints =>
      // {
      //   endpoints.MapControllers();
      // });
    }
  }
}
