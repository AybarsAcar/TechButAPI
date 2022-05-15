using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TechBuyAPI
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      
      // get access to the data context
      using (var scope = host.Services.CreateScope())
      {
        // get services
        var services = scope.ServiceProvider;
        
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
          var context = services.GetRequiredService<StoreContext>();

          // apply any pending migrations to the database and create database if it does not exist
          await context.Database.MigrateAsync();
          
          // seed data
          await StoreContextSeed.SeedAsync(context, loggerFactory);
        }
        catch (Exception e)
        {
          var logger = loggerFactory.CreateLogger<Program>();
          
          logger.LogError(e, "An error occured during migration");
          throw;
        }
      }
      
      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}