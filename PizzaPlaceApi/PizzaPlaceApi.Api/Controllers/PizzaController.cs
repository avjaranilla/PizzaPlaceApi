using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Application.Services;
using PizzaPlaceApi.Domain.Entities;

namespace PizzaPlaceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly IPizzaTypeService _pizzaTypeService;

        public PizzaController(IPizzaService pizzaService, IPizzaTypeService pizzaTypeService)
        {
            _pizzaService = pizzaService;
            _pizzaTypeService = pizzaTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            var pizzas = await _pizzaService.GetAllPizzasAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(string id)
        {
            var pizza = await _pizzaService.GetPizzaByIdAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpPost]
        public async Task<ActionResult<Pizza>> CreatePizza(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure PizzaTypeId is provided
            if (string.IsNullOrEmpty(pizza.PizzaTypeId))
            {
                return BadRequest("PizzaTypeId is required.");
            }

            // Check if PizzaType exists
            var pizzaType = await _pizzaTypeService.GetPizzaTypeByIdAsync(pizza.PizzaTypeId);
            if (pizzaType == null)
            {
                return BadRequest("Invalid PizzaTypeId.");
            }

            // Add the pizza
            await _pizzaService.AddPizzaAsync(pizza);
            return CreatedAtAction(nameof(GetPizza), new { id = pizza.PizzaId }, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizza(string id, Pizza pizza)
        {
            if (id != pizza.PizzaId)
            {
                return BadRequest();
            }

            await _pizzaService.UpdatePizzaAsync(pizza);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(string id)
        {
            await _pizzaService.DeletePizzaAsync(id);
            return NoContent();
        }
    }
}
