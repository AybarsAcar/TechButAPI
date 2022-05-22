namespace Core.Entities.OrderAggregate
{
  /// <summary>
  /// snapshot of the product item at the place we order it
  /// i.e. if the price changes for a ProductItem we don't that to reflect on the
  /// Product Item already on the order
  /// </summary>
  public class ProductItemOrdered
  {
    public int ProductItemId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }

    public ProductItemOrdered()
    {
    }

    public ProductItemOrdered(int productItemId, string productName, string imageUrl)
    {
      ProductItemId = productItemId;
      ProductName = productName;
      ImageUrl = imageUrl;
    }
  }
}