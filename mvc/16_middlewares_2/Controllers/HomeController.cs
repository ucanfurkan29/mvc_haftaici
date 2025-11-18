using _16_middlewares_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _16_middlewares_2.Controllers
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
            ViewBag.Message = "Bu sayfa hýzlý yüklenir (0-100ms)";
            return View();
        }
        public async Task<IActionResult> SlowPage()
        {
            await Task.Delay(2000);
            ViewBag.Message = "Bu sayfa yavaþ yüklenir (2000ms+)";
            ViewBag.Title = "Yavaþ Sayfa";
            return View("Index");
        }

        public IActionResult Privacy()
        {
            ViewBag.Message = "privacy sayfasý orta hýzda yüklenir";
            ViewBag.Title= "Privacy";
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ApiTest()
        {
            return Json(new
            {
                message = "Apý çaðrýsý baþarýlý",
                timestamp = DateTime.Now,
                server = Environment.MachineName
            });
        }
    }
}
