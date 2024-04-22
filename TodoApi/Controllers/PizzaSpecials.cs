using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    public class PizzaSpecials : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
