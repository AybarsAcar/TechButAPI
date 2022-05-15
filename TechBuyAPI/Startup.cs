using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

      services.AddDbContext<StoreContext>(opt => { opt.UseNpgsql(_config.GetConnectionString("DefaultConnection")); });
      
      services.AddAutoMapper(typeof(MappingProfiles));

      services.AddApplicationServices();
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

      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}