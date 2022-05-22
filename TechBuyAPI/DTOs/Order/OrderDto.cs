using TechBuyAPI.DTOs.Account;

namespace TechBuyAPI.DTOs.Order
{
  public class OrderDto
  {
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
  }
}