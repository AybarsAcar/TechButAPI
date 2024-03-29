using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TechBuyAPI.Errors;
using TechBuyAPI.Utils;

namespace TechBuyAPI.Middleware;

/// <summary>
/// Middleware to handle the exception
/// </summary>
public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly IHostEnvironment _env;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
  {
    _next = next;
    _logger = logger;
    _env = env;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      // if there is no exception move onto the next stage
      await _next(context);
    }
    catch (Exception e)
    {
      _logger.LogError(e, e.Message);

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      // write out a response
      var response = _env.IsDevelopment()
        ? new ApiException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace)
        : new ApiException((int)HttpStatusCode.InternalServerError);

      var options = new JsonSerializerOptions
      {
        PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
      };

      var json = JsonSerializer.Serialize(response, options);

      await context.Response.WriteAsync(json);
    }
  }
}