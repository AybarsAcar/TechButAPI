using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
  public interface IOrderService
  {
    /// <summary>
    /// Creates an order
    /// we trust the items and their quantities from the basket but we get the actual price from our database
    /// never trust the price sent from the client
    /// </summary>
    /// <param name="buyerEmail"></param>
    /// <param name="deliveryMethodId"></param>
    /// <param name="basketId"></param>
    /// <param name="shippingAddress"></param>
    /// <returns></returns>
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);

    /// <summary>
    /// returns all the user orders with their emails
    /// </summary>
    /// <param name="buyerEmail"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="buyerEmail"></param>
    /// <returns></returns>
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

    /// <summary>
    /// Convenience method to get all the delivery methods
    /// </summary>
    /// <returns></returns>
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
  }
}