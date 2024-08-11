using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.Entities;

namespace PizzaPlaceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaTypeController : Controller
    {
        private readonly IPizzaTypeService _pizzaTypeService;

        public PizzaTypeController(IPizzaTypeService pizzaTypeService)
        {
            _pizzaTypeService = pizzaTypeService;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaType>>> GetPizzaTypes()
        {
            var pizzaTypes = await _pizzaTypeService.GetAllPizzaTypesAsync();
            return Ok(pizzaTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaType>> GetPizzaType(string id)
        {
            var pizzaType = await _pizzaTypeService.GetPizzaTypeByIdAsync(id);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return Ok(pizzaType);
        }

        [HttpPost]
        public async Task<ActionResult<PizzaType>> CreatePizzaType(PizzaType pizzaType)
        {
            // Initialize the PizzaType object
            if (pizzaType == null)
            {
                return BadRequest();
            }

            // Add the new PizzaType to the database
            await _pizzaTypeService.AddPizzaTypeAsync(pizzaType);
            return CreatedAtAction(nameof(GetPizzaType), new { id = pizzaType.PizzaTypeId }, pizzaType);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizzaType(string id, PizzaType pizzaType)
        {
            if (id != pizzaType.PizzaTypeId)
            {
                return BadRequest();
            }

            var updatedPizzaType = await _pizzaTypeService.UpdatePizzaTypeAsync(pizzaType);

            // Check if the update was successful
            if (updatedPizzaType == null)
            {
                return NotFound("Pizza not found.");
            }

            // Return the updated pizza details
            return Ok(updatedPizzaType);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaType(string id)
        {
            await _pizzaTypeService.DeletePizzaTypeAsync(id);
            return NoContent();
        }


    }
}
