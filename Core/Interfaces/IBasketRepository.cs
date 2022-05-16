using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  /// <summary>
  /// repository that works with Redis
  /// </summary>
  public interface IBasketRepository
  {
    /// <summary>
    /// Returns a Customer Basket from Redis Cache given a basketId
    /// </summary>
    /// <param name="baskedId"></param>
    /// <returns></returns>
    Task<CustomerBasket> GetBasketAsync(string baskedId);

    /// <summary>
    /// updates the basket if a basket with the passed in basket's id exists
    /// or creates a new basked if a basket with the passed in basket's id does not exist
    /// </summary>
    /// <param name="basket"></param>
    /// <returns></returns>
    Task<CustomerBasket> CreateUpdateBasketAsync(CustomerBasket basket);

    /// <summary>
    /// deletes the basket from Redis Cache given a basketId
    /// </summary>
    /// <param name="basketId"></param>
    /// <returns></returns>
    Task<bool> DeleteBasketAsync(string basketId);
  }
}