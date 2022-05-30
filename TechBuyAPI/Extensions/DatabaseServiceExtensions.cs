using System;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechBuyAPI.Extensions
{
  public static class DatabaseServiceExtensions
  {
    public static IServiceCollection AddPostgresDatabaseServices(this IServiceCollection services,
      IConfiguration config)
    {
      // SETUP STORE DATABASE
      services.AddDbContext<StoreContext>(options =>
      {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        string connStr;

        // Depending on if in development or production, use either Heroku-provided
        // connection string, or development connection string from env var.
        if (env == "Development")
        {
          // Use connection string from appsettings.Development.json
          connStr = config.GetConnectionString("DefaultConnection");
        }
        else
        {
          // Following executes in Production
          // Use connection string provided at runtime by Heroku.
          var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

          // Parse connection URL to connection string for Npgsql
          connUrl = connUrl.Replace("postgres://", string.Empty);
          var pgUserPass = connUrl.Split("@")[0];
          var pgHostPortDb = connUrl.Split("@")[1];
          var pgHostPort = pgHostPortDb.Split("/")[0];
          var pgDb = pgHostPortDb.Split("/")[1];
          var pgUser = pgUserPass.Split(":")[0];
          var pgPass = pgUserPass.Split(":")[1];
          var pgHost = pgHostPort.Split(":")[0];
          var pgPort = pgHostPort.Split(":")[1];

          connStr =
            $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}; SSL Mode=Require; Trust Server Certificate=true";
        }

        // Whether the connection string came from the local development configuration file
        // or from the environment variable from Heroku, use it to set up your DbContext.
        options.UseNpgsql(connStr);
      });

      // SETUP IDENTITY DATABASE
      services.AddDbContext<AppIdentityDbContext>(options =>
      {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        string connStr;

        // Depending on if in development or production, use either Heroku-provided
        // connection string, or development connection string from env var.
        if (env == "Development")
        {
          // Use connection string from appsettings.Development.json
          connStr = config.GetConnectionString("IdentityConnection");
        }
        else
        {
          // Following executes in Production
          // Use connection string provided at runtime by Heroku.
          var connUrl = Environment.GetEnvironmentVariable("HEROKU_POSTGRESQL_OLIVE_URLs");

          // Parse connection URL to connection string for Npgsql
          connUrl = connUrl.Replace("postgres://", string.Empty);
          var pgUserPass = connUrl.Split("@")[0];
          var pgHostPortDb = connUrl.Split("@")[1];
          var pgHostPort = pgHostPortDb.Split("/")[0];
          var pgDb = pgHostPortDb.Split("/")[1];
          var pgUser = pgUserPass.Split(":")[0];
          var pgPass = pgUserPass.Split(":")[1];
          var pgHost = pgHostPort.Split(":")[0];
          var pgPort = pgHostPort.Split(":")[1];

          connStr =
            $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}; SSL Mode=Require; Trust Server Certificate=true";
        }

        // Whether the connection string came from the local development configuration file
        // or from the environment variable from Heroku, use it to set up your DbContext.
        options.UseNpgsql(connStr);
      });
      

      return services;
    }
  }
}

/*
services.AddDbContext<DataContext>(options =>
{
  var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

  string connStr;

  // Depending on if in development or production, use either Heroku-provided
  // connection string, or development connection string from env var.
  if (env == "Development")
  {
    // Use connection string from appsettings.Development.json
    connStr = config.GetConnectionString("DefaultConnection");
  }
  else
  {
    // Following executes in Production
    // Use connection string provided at runtime by Heroku.
    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    // Parse connection URL to connection string for Npgsql
    connUrl = connUrl.Replace("postgres://", string.Empty);
    var pgUserPass = connUrl.Split("@")[0];
    var pgHostPortDb = connUrl.Split("@")[1];
    var pgHostPort = pgHostPortDb.Split("/")[0];
    var pgDb = pgHostPortDb.Split("/")[1];
    var pgUser = pgUserPass.Split(":")[0];
    var pgPass = pgUserPass.Split(":")[1];
    var pgHost = pgHostPort.Split(":")[0];
    var pgPort = pgHostPort.Split(":")[1];

    connStr =
      $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}; SSL Mode=Require; Trust Server Certificate=true";
  }

  // Whether the connection string came from the local development configuration file
  // or from the environment variable from Heroku, use it to set up your DbContext.
  options.UseNpgsql(connStr);
});
*/