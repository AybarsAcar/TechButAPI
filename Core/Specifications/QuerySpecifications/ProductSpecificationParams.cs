using System;

namespace Core.Specifications.QuerySpecifications
{
  /// <summary>
  /// Query parameters passed down when querying the products
  /// contains all query parameters, their constraints, and default values
  /// </summary>
  public class ProductSpecificationParams
  {
    private const int MaxPageSize = 50;

    // by default return the very fist page
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;

    public int PageSize
    {
      get => _pageSize;
      set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int? BrandId { get; set; }
    
    public int? TypeId { get; set; }

    public string Sort { get; set; }

    private string _search;

    public string Search
    {
      get => _search; 
      set => _search = value.ToLower();
    }
  }
}