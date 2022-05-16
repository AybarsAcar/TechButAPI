using Core.Entities;

namespace Core.Specifications.QuerySpecifications
{
  public class ProductsWithTypesAndBrands : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrands(ProductSpecificationParams productParams)
      : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
      )
    {
      // add product types and brands
      AddInclude(p => p.ProductBrand);
      AddInclude(p => p.ProductType);

      // default ordering is by Product Name
      AddOrderBy(p => p.Name);

      ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

      if (!string.IsNullOrEmpty(productParams.Sort))
      {
        switch (productParams.Sort)
        {
          case "priceAsc":
            AddOrderBy(p => p.Price);
            break;

          case "priceDesc":
            AddOrderByDescending(p => p.Price);
            break;

          default:
            AddOrderBy(p => p.Name);
            break;
        }
      }
    }

    /// <summary>
    /// Used to return a Single Product by its Primary Key
    /// </summary>
    /// <param name="id">Primary Key</param>
    public ProductsWithTypesAndBrands(int id) : base(
      x => x.Id == id
    )
    {
      // add product types and brands
      AddInclude(x => x.ProductBrand);
      AddInclude(x => x.ProductType);
    }
  }
}