using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TechBuyAPI.Extensions;
using TechBuyAPI.Mappers;
using TechBuyAPI.Middleware;
using TechBuyAPI.Utils;

namespace TechBuyAPI
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration configuration)
    {
      _config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
        });

      services.AddSwaggerDocumentation();

      // setup Postgresql
      services.AddPostgresDatabaseServices(_config);

      // setup Redis
      services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(c =>
      {
        var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);

        return ConnectionMultiplexer.Connect(configuration);
      });

      services.AddAutoMapper(typeof(MappingProfiles));

      services.AddApplicationServices();

      services.AddIdentityServices(_config);

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
          policy =>
          {
            policy
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("WWW-Authenticate", "Pagination")
              .AllowAnyOrigin();
          });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // add custom middleware
      app.UseMiddleware<ExceptionMiddleware>();

      // Add Swagger API documentation
      app.UseSwaggerDocumentation();

      // when we don't have an endpoint for the request
      // we are redirected here
      app.UseStatusCodePagesWithReExecute("/errors/{0}");

      app.UseHttpsRedirection();

      app.UseRouting();

      // add static files - wwwroot folder
      app.UseStaticFiles();

      app.UseCors("CorsPolicy");

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}