using Microsoft.AspNetCore.Mvc;

namespace PizzaPlaceApi.Api.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
