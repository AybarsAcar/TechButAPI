using System.Collections.Generic;

namespace Core.Entities
{
  public class CustomerBasket
  {
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new();

    public int? DeliveryMethodId { get; set; }

    public string ClientSecret { get; set; }

    public string PaymentIntentId { get; set; }

    public CustomerBasket()
    {
    }

    public CustomerBasket(string id)
    {
      Id = id;
    }
  }
}