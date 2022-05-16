using Core.Entities;

namespace Core.Specifications.QuerySpecifications
{
  /// <summary>
  /// to get the count of the products in the returned data
  /// so we can populate the response
  /// </summary>
  public class ProductWithFiltersForCount : BaseSpecification<Product>
  {
    public ProductWithFiltersForCount(ProductSpecificationParams productParams)
      : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
      )
    {
    }
  }
}