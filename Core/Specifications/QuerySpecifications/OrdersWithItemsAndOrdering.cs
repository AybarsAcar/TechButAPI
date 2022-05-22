using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications.QuerySpecifications
{
  public class OrdersWithItemsAndOrdering : BaseSpecification<Order>
  {
    /// <summary>
    /// used for a list of order for the user
    /// </summary>
    /// <param name="email">Buyer Email</param>
    public OrdersWithItemsAndOrdering(string email) : base(o => o.BuyerEmail == email)
    {
      AddInclude(o => o.OrderItems);
      AddInclude(o => o.DeliveryMethod);

      AddOrderByDescending(o => o.OrderDate);
    }

    /// <summary>
    /// used for retrieving a single email
    /// </summary>
    /// <param name="id">Order id</param>
    /// <param name="email">Buyer Email</param>
    public OrdersWithItemsAndOrdering(int id, string email) : base(
      o => o.Id == id && o.BuyerEmail == email
    )
    {
      AddInclude(o => o.OrderItems);
      AddInclude(o => o.DeliveryMethod);
    }
  }
}