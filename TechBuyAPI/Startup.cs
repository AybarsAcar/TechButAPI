using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TechBuyAPI.Mappers;
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

      services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechBuyAPI", Version = "v1" }); });

      services.AddDbContext<StoreContext>(opt => { opt.UseNpgsql(_config.GetConnectionString("DefaultConnection")); });

      services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

      services.AddAutoMapper(typeof(MappingProfiles));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechBuyAPI v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      
      // add static files - wwwroot folder
      app.UseStaticFiles();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}