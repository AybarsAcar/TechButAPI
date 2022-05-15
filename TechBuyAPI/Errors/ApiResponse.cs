namespace TechBuyAPI.Errors
{
  public class ApiResponse
  {
    public ApiResponse(int statusCode, string message = null)
    {
      StatusCode = statusCode;
      Message = message ?? DefaultMessageForStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    
    private string DefaultMessageForStatusCode(int statusCode)
    {
      return statusCode switch
      {
        400 => "Received a bad request.",
        401 => "Client not authorized",
        404 => "Resource not found",
        500 => "Unexpected Server Error",
        _ => "Unexpected Error"
      };
    }

  }
}