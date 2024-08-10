using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfaces;

namespace PizzaPlaceApi.Api.Controllers
{
    public class ImportDataController : Controller
    {
        private readonly ICsvImportService _csvImportService;

        public ImportDataController(ICsvImportService csvImportService)
        {
            _csvImportService = csvImportService;
        }

        [HttpPost("import-pizza")]
        public async Task<IActionResult> ImportPizza([FromForm] string filePath)
        {
            await _csvImportService.ImportPizzaDataAsync(filePath);
            return Ok("Pizza data imported successfully.");
        }

        [HttpPost("import-pizzatype")]
        public async Task<IActionResult> ImportPizzaType([FromForm] string filePath)
        {
            await _csvImportService.ImportPizzaTypeDataAsync(filePath);
            return Ok("Pizza types data imported successfully.");
        }

        [HttpPost("import-orders")]
        public async Task<IActionResult> ImportOrders([FromForm] string filePath)
        {
            await _csvImportService.ImportOrdersDataAsync(filePath);
            return Ok("Orders data imported successfully.");
        }

        [HttpPost("import-orderdetails")]
        public async Task<IActionResult> ImportOrderDetails([FromForm] string filePath)
        {
            await _csvImportService.ImportOrderDetailsDataAsync(filePath);
            return Ok("Order details data imported successfully.");
        }
    }
}
