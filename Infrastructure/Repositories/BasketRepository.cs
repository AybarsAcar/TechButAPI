using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Repositories
{
  public class BasketRepository : IBasketRepository
  {
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
      _database = redis.GetDatabase();
    }

    public async Task<CustomerBasket> GetBasketAsync(string baskedId)
    {
      var data = await _database.StringGetAsync(baskedId);

      return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> CreateUpdateBasketAsync(CustomerBasket basket)
    {
      // basket is stored for up to maximum of 30 days
      var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

      if (!created) return null;

      return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
      return await _database.KeyDeleteAsync(basketId);
    }
  }
}