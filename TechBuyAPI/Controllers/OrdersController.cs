using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBuyAPI.DTOs.Account;
using TechBuyAPI.DTOs.Order;
using TechBuyAPI.Errors;

namespace TechBuyAPI.Controllers
{
  [Authorize]
  public class OrdersController : BaseApiController
  {
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
      _orderService = orderService;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
      // get the user email from JWT
      var email = User.FindFirstValue(ClaimTypes.Email);

      var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

      var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

      if (order == null)
      {
        return BadRequest(new ApiResponse(400, "Problem creating order"));
      }

      return order;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
    {
      // get the user email from JWT
      var email = User.FindFirstValue(ClaimTypes.Email);

      var orders = await _orderService.GetOrdersForUserAsync(email);

      var ordersToReturn = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

      return Ok(ordersToReturn);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
    {
      // get the user email from JWT
      var email = User.FindFirstValue(ClaimTypes.Email);

      var order = await _orderService.GetOrderByIdAsync(id, email);

      if (order == null)
      {
        return NotFound(new ApiResponse(404, "Order does not exist"));
      }

      return _mapper.Map<Order, OrderToReturnDto>(order);
    }

    [HttpGet("delivery-methods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
      var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
      return Ok(deliveryMethods);
    }
  }
}