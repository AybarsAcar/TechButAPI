using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechBuyAPI.Controllers
{
  public class PaymentsController : BaseApiController
  {
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
      _paymentService = paymentService;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateUpdatePaymentIntent(string basketId)
    {
      return await _paymentService.CreateUpdatePaymentIntent(basketId);
    }
  }
}