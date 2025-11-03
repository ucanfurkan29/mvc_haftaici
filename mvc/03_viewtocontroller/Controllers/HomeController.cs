using _03_viewtocontroller.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _03_viewtocontroller.Controllers
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
            return View();
        }

        [HttpPost]
        public IActionResult Index(string ad,string kisiler,bool onay = false)
        {
            var k1 = Request.Form["kisiler"];
            var a1 = Request.Form["ad"];
            var o1 = Request.Form["onay"];

            ViewBag.name = a1;

            return View();
        }
    }
}
