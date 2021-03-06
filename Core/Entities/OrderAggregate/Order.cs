using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
  /// <summary>
  /// this will have its own table in the database
  /// </summary>
  public class Order : BaseEntity
  {
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public Address ShipToAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItem> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string PaymentIntentId { get; set; }

    public Order()
    {
    }

    public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod,
      IReadOnlyList<OrderItem> orderItems, decimal subtotal)
    {
      BuyerEmail = buyerEmail;
      ShipToAddress = shipToAddress;
      DeliveryMethod = deliveryMethod;
      OrderItems = orderItems;
      Subtotal = subtotal;
    }

    public decimal GetTotal()
    {
      return Subtotal + DeliveryMethod.Price;
    }
  }
}