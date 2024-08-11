using Microsoft.AspNetCore.Mvc;
using PizzaPlaceApi.Application.Interfaces;

namespace PizzaPlaceApi.Api.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet("top-selling-pizzas")]
        public async Task<IActionResult> GetTopSellingPizzas([FromQuery] int topN = 10)
        {
            if (topN <= 0)
            {
                return BadRequest("The number of top pizzas must be greater than zero.");
            }

            var topSellingPizzas = await _reportService.GetTopSellingPizzasAsync(topN);

            if (topSellingPizzas == null || !topSellingPizzas.Any())
            {
                return NotFound("No top-selling pizzas found.");
            }

            return Ok(topSellingPizzas);
        }

        [HttpGet("daily-sales")]
        public async Task<IActionResult> GetDailySales([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            // Validate the date range
            if (endDate < startDate)
            {
                return BadRequest("End date cannot be earlier than start date.");
            }

            try
            {
                var dailySales = await _reportService.GetSalesReportByDateRangeAsync(startDate, endDate);
                return Ok(dailySales);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library or framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
