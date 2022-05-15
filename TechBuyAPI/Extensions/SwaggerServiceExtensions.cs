using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TechBuyAPI.Extensions
{
  public static class SwaggerServiceExtensions
  {
    
    /// <summary>
    /// Service Injection for Swagger Documentation
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
      services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechBuyAPI", Version = "v1" }); });

      return services;
    }

    /// <summary>
    /// Extension on the IApplicationBuilder
    /// Custom Swagger middleware
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechBuyAPI v1"));
      return app;
    }
  }
}