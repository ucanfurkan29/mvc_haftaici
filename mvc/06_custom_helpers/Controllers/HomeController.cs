using _06_custom_helpers.Helpers;
using _06_custom_helpers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _06_custom_helpers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var formatted = StringHelper.CapitalizeWord("databaseden gelen veri");
            ViewBag.Message = formatted;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
