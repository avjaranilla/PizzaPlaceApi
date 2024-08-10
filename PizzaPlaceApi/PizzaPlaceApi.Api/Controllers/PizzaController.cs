using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfraces;

namespace PizzaPlaceApi.Api.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ICsvImportService _csvImportService;

        public PizzaController(ICsvImportService csvImportService)
        {
            _csvImportService = csvImportService;
        }

        //[HttpPost("import-pizza")]
        //public async Task<IActionResult> ImportPizza([FromForm] string filePath)
        //{
        //    await _csvImportService.ImportPizzaDataAsync(filePath);
        //    return Ok("Pizza data imported successfully.");
        //}
    }
}
