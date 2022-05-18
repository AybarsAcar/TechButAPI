using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBuyAPI.Errors;

namespace TechBuyAPI.Controllers
{
  public class ErrorTestController : BaseApiController
  {
    private readonly StoreContext _context;

    public ErrorTestController(StoreContext context)
    {
      _context = context;
    }

    [HttpGet("not-found")]
    public ActionResult GetNotFoundRequest()
    {
      return NotFound(new ApiResponse(404));
    }

    [HttpGet("server-error")]
    public ActionResult GetServerErrorRequest()
    {
      var thing = _context.Products.Find(-1);

      // this line will throw server error
      var thingToReturn = thing.Name;

      return Ok();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequestRequest()
    {
      return BadRequest(new ApiResponse(400));
    }

    [HttpGet("bad-request/{id}")]
    public ActionResult GetBadRequestRequest(int id)
    {
      return Ok();
    }

    [HttpGet("test-auth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
      return "This is a secret string";
    }
  }
}