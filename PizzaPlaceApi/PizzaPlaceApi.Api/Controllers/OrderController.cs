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
        private readonly IPizzaService _pizzaService;

        public OrderController(IOrderService orderService, IPizzaService pizzaService)
        {
            _orderService = orderService;
            _pizzaService = pizzaService;
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
                var createdOrder = await _orderService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating order: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO updateOrderDto)
        {
            // Validate the input
            if (updateOrderDto == null)
            {
                return BadRequest("Order data is required.");
            }

            if (updateOrderDto.OrderDetails == null || updateOrderDto.OrderDetails.Count == 0)
            {
                return BadRequest("Order details are required.");
            }

            //Check if pizza IDs are valid
            foreach (var detail in updateOrderDto.OrderDetails)
            {
                if (string.IsNullOrEmpty(detail.PizzaId))
                {
                    return BadRequest("Pizza ID is required.");
                }

                //Validate Pizza ID exists -this could be done through a service call
                var pizzaExists = await _pizzaService.GetPizzaByIdAsync(detail.PizzaId);
                if (pizzaExists == null)
                {
                    return BadRequest($"Pizza with ID {detail.PizzaId} does not exist.");
                }
            }

            try
            {
                var updatedOrder = await _orderService.UpdateOrderAsync(updateOrderDto);
                if (updatedOrder == null)
                {
                    return NotFound($"Order with ID {updateOrderDto.OrderId} not found.");
                }

                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                await _orderService.DeleteOrderByIdAsync(orderId);
                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message }); // Return 404 if the order is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message }); // Return 500 for any other errors
            }
        }


    }
}
