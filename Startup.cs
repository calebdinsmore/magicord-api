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
using Magicord.Modules.Booster;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Magicord.Core.ApiKeyAuthorization;
using Magicord.Modules.Trivia;
using Magicord.Modules.Shop;
using Magicord.Modules.AdminProcess;
using Magicord.Modules.Cards;

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
        .AddAuthorization()
        .AddQueryType<Query>()
        .AddMutationType<Mutation>()
        .AddProjections()
        .ModifyRequestOptions(o => o.ExecutionTimeout = TimeSpan.FromSeconds(10800));

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

      services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        opt.AddScheme<ApiKeyAuthenticationHandler>(ApiKeyDefaults.AuthenticationScheme, ApiKeyDefaults.AuthenticationScheme);
      })
      .AddJwtBearer(opt =>
      {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["ConfigSettings:Jwt:Key"])),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IBoosterService, BoosterService>();
      services.AddScoped<ITriviaGeneratorFactory, TriviaGeneratorFactory>();
      services.AddScoped<IShopService, ShopService>();
      services.AddScoped<IAdminProcessService, AdminProcessService>();
      services.AddScoped<ICardService, CardService>();
      services.AddSingleton<Random>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware(typeof(HttpErrorHandlingMiddleware));

      // app.UseHttpsRedirection();


      app.UseCors(MyAllowSpecificOrigins);
      app.UseAuthentication();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
        {
          endpoints.MapGraphQL();
          endpoints.MapControllers();
        });
    }
  }
}
