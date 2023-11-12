using Core.Entities;
using E_Commerce.HandleResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.OrderService;
using Services.Services.OrderService.Dto;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto orderDto)
        {

            var order = await _orderService.CreateOrderAsync(orderDto);

            if (order is null)
                return BadRequest(new ApiResponse(400, "Error While Creating your order"));
            
            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersForUserAsync()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetAllOrdersForUserAsync(email);
            
            if (orders is { Count: <= 0 })
                return Ok(new ApiResponse(200, "You Dont't have any orders yet!"));


            return Ok(orders);
        }


        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(int id)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderByIdAsync(id,email);

            if (orders is null)
                return Ok(new ApiResponse(200, $"There is no order with {id}"));
            return Ok(orders);    
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodsAsync()
            => Ok(await _orderService.GetAllDeliveryMethodsAsync());
    }
}
    