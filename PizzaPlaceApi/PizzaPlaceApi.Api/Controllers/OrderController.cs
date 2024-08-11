using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.DTOs;

namespace PizzaPlaceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : Controller
    {
       private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var orders = await _orderService.GetAllOrdersAsync(pageNumber, pageSize);
            return Ok(orders);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            if (createOrderDto == null)
            {
                return BadRequest("Order data is null.");
            }

            try
            {
                await _orderService.CreateOrderAsync(createOrderDto);
                return Ok("Order created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating order: {ex.Message}");
            }
        }


    }
}
