using Microsoft.AspNetCore.Mvc;

namespace _17_filter_operation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
