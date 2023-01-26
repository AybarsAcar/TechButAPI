using System.Collections.Generic;

namespace TechBuyAPI.Utils;

/// <summary>
/// wrapper class for the response from the API that returns a paginated response to the client
/// it will store and respond with the pagination meta-data
/// </summary>
/// <typeparam name="T">Any Data</typeparam>
public class PaginatedResponse<T> where T : class
{
  public int PageIndex { get; set; }

  public int PageSize { get; set; }

  public int PageCount { get; set; }

  public IReadOnlyList<T> Data { get; set; }

  public PaginatedResponse(int pageIndex, int pageSize, int pageCount, IReadOnlyList<T> data)
  {
    PageIndex = pageIndex;
    PageSize = pageSize;
    PageCount = pageCount;
    Data = data;
  }
}