namespace TechBuyAPI.DTOs.Order
{
  public class OrderItemDto
  {
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
  }
}