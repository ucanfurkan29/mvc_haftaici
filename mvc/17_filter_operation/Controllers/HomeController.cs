using _17_filter_operation.Filters;
using _17_filter_operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Security.Permissions;

namespace _17_filter_operation.Controllers
{
    public class HomeController : Controller
    {
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult Index()
        {
            return View();
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        public IActionResult Privacy()
        {
            return View();
        }

        [ServiceFilter(typeof(ExceptionFilter))]
        public IActionFilter SpecialAction()
        {
            throw new Exception("Bu bir test hatasýdýr");
        }

    }
}
