using _05_html_helpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace _05_html_helpers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = new User();
            user.CountryList = GetCountries();

            user.Name = "Furkan";

            return View(user);
        }

        [HttpPost]
        public IActionResult Submit(User user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = $"Name {user.Name} - Age: {user.Age} - Gender: {user.Gender} - Country: {user.Country}";
                return View("Result");
            }
            user.CountryList = GetCountries();
            return View("Index");
        }

        public IActionResult Result()
        {
            return View();
        }




        public IEnumerable<SelectListItem> GetCountries()
        {
            return new SelectListItem[]
            {
                new SelectListItem{Value = "TR", Text = "Türkiye"},
                new SelectListItem{Value = "US", Text = "USA"},
                new SelectListItem{Value = "JP", Text = "Japonya"}
            };
        }

    }
}
