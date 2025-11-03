
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02_controllertoview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = new List<string> { "Ürün 1", "Ürün 2", "Ürün 3","Ürün 4" };
            ViewData["Products"] = products;
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = $"Ürün {id} Detaylar";
            ViewData["Product"] = product;
            return View();
        }
    }
}
