using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfraces;

namespace PizzaPlaceApi.Api.Controllers
{
    public class PizzaTypeController : Controller
    {
        private readonly ICsvImportService _csvImportService;

        public PizzaTypeController(ICsvImportService csvImportService)
        {
            _csvImportService = csvImportService;
        }

        //[HttpPost("import-pizzatype")]
        //public async Task<IActionResult> ImportPizzaType([FromForm] string filePath)
        //{
        //    await _csvImportService.ImportPizzaTypeDataAsync(filePath);
        //    return Ok("Pizza types data imported successfully.");
        //}
    }
}
