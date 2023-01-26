using System.Linq;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TechBuyAPI.Errors;

namespace TechBuyAPI.Extensions;

public static class ApplicationServicesExtensions
{
  /// <summary>
  /// Extension method on the IServiceCollection
  /// to tidy up the Startup.cs
  /// this method contains custom dependencies and configurations injected to the application
  /// </summary>
  /// <param name="services"></param>
  /// <returns></returns>
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.InvalidModelStateResponseFactory = actionContext =>
      {
        var errors = actionContext.ModelState
          .Where(e => e.Value.Errors.Count > 0)
          .SelectMany(x => x.Value.Errors)
          .Select(x => x.ErrorMessage)
          .ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
          Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
      };
    });

    // add repositories
    services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    services.AddScoped<IBasketRepository, BasketRepository>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // add services
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IPaymentService, PaymentService>();

    return services;
  }
}