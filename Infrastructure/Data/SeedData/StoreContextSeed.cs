using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.SeedData
{
  /// <summary>
  /// called from the Program class to seed data
  /// this class has no access to the application context and services
  /// </summary>
  public class StoreContextSeed
  {
    private StoreContextSeed()
    {
    }

    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
      try
      {
        if (!context.ProductBrands.Any())
        {
          var brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");

          // serialise the string
          var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

          // add to the database via the context
          foreach (var item in brands)
          {
            context.ProductBrands.Add(item);
          }

          // submit changes to database 
          await context.SaveChangesAsync();
        }
        
        if (!context.ProductTypes.Any())
        {
          var typesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");

          // serialise the string
          var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

          // add to the database via the context
          foreach (var item in types)
          {
            context.ProductTypes.Add(item);
          }

          // submit changes to database 
          await context.SaveChangesAsync();
        }
        
        if (!context.Products.Any())
        {
          var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

          // serialise the string
          var products = JsonSerializer.Deserialize<List<Product>>(productsData);

          // add to the database via the context
          foreach (var item in products)
          {
            context.Products.Add(item);
          }

          // submit changes to database 
          await context.SaveChangesAsync();
        }
      }
      catch (Exception e)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogError(e, "Error seeding data to database");
        throw;
      }
    }
  }
}