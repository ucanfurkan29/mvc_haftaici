using _15_middlewares.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _15_middlewares.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var middlewareMessage = HttpContext.Items["MiddlewareMessage"]?.ToString();
            //?. ifadesi "MiddlewareMessage anahtarýnýn HttpContext.Items içerisnde bulunup bulunmadýðýný kontrol eder.
            //eðer buluyorsa tostring metodunu çaðýrýr, bulamazsa null deðer döndürür

            ViewBag.MiddlewareInfo = middlewareMessage; //viewbag ile view e gönderdik
            return View();
        }

    }
}
