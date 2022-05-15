using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.QuerySpecifications
{
  public class ProductsWithTypesAndBrands : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrands()
    {
      // add product types and brands
      AddInclude(x => x.ProductBrand);
      AddInclude(x => x.ProductType);
    }

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