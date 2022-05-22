using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications.QuerySpecifications;

namespace Infrastructure.Services
{
  public class OrderService : IOrderService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepository;

    public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
    {
      _unitOfWork = unitOfWork;
      _basketRepository = basketRepository;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
      Address shippingAddress)
    {
      // get basket from basket repository
      var basket = await _basketRepository.GetBasketAsync(basketId);

      // get items from the product repository
      var items = new List<OrderItem>();
      foreach (var basketItem in basket.Items)
      {
        var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(basketItem.Id);
        var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ImageUrl);
        var orderItem = new OrderItem(itemOrdered, productItem.Price, basketItem.Quantity);
        items.Add(orderItem);
      }

      // get delivery method from repository
      var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

      // calc subtotal
      var subtotal = items.Sum(item => item.Price * item.Quantity);

      // create order
      var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);

      // add the new order
      _unitOfWork.Repository<Order>().Add(order);

      // commit the changes to the database
      var result = await _unitOfWork.Complete();

      if (result <= 0)
      {
        return null;
      }

      // order is saved successfully
      // delete the basket
      await _basketRepository.DeleteBasketAsync(basketId);

      // return created order
      return order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
      var spec = new OrdersWithItemsAndOrdering(buyerEmail);

      return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
      var spec = new OrdersWithItemsAndOrdering(id, buyerEmail);

      return await _unitOfWork.Repository<Order>().GetWithSpecification(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
      return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }
  }
}