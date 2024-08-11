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
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path is required.");
            }
            var importedCount = await _csvImportService.ImportPizzaDataAsync(filePath);
            return Ok(new
            {
                Message = "Pizzas data imported successfully.",
                Count = importedCount
            });
        }

        [HttpPost("import-pizzatype")]
        public async Task<IActionResult> ImportPizzaType([FromForm] string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path is required.");
            }

            var importedCount = await _csvImportService.ImportPizzaTypeDataAsync(filePath);

            return Ok(new
            {
                Message = "Pizza types data imported successfully.",
                Count = importedCount
            });
        }

        [HttpPost("import-orders")]
        public async Task<IActionResult> ImportOrders([FromForm] string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path is required.");
            }

            var importedCount = await _csvImportService.ImportOrdersDataAsync(filePath);
            return Ok(new
            {
                Message = "Orders data imported successfully.",
                Count = importedCount
            });
        }

        [HttpPost("import-orderdetails")]
        public async Task<IActionResult> ImportOrderDetails([FromForm] string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path is required.");
            }

            var importedCount = await _csvImportService.ImportOrderDetailsDataAsync(filePath);
            return Ok(new
            {
                Message = "Orders details data imported successfully.",
                Count = importedCount
            });
        }
    }
}
