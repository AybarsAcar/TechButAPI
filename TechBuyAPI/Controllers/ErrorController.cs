using Microsoft.AspNetCore.Mvc;
using TechBuyAPI.Errors;

namespace TechBuyAPI.Controllers
{
  [Route(("errors/{code}"))]
  public class ErrorController : BaseApiController
  {
    [HttpGet]
    public IActionResult Error(int code)
    {
      return new ObjectResult(new ApiResponse(code));
    }
  }
}