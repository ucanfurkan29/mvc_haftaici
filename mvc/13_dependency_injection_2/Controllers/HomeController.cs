using _12_Dependency_Injection_2.Models;
using _12_Dependency_Injection_2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _12_Dependency_Injection_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly TransientRandomNumberService _transientService;
        private readonly TransientRandomNumberService _transientService1;
        private readonly ScopedHelperService _scopedHelperService;
        private readonly ScopedHelperService _scopedHelperService1;
        private readonly SingletonRandomNumberService _singletonService;
        private readonly SingletonRandomNumberService _singletonService1;
        public HomeController(TransientRandomNumberService trasientService, TransientRandomNumberService trasientService1,ScopedHelperService scopedHelperService,ScopedHelperService scopedHelperService1,SingletonRandomNumberService singletonRandomNumberService,SingletonRandomNumberService singletonRandomNumberService1)
        {
            _transientService= trasientService;
            _transientService1= trasientService1;
            _scopedHelperService= scopedHelperService;
            _scopedHelperService1= scopedHelperService1;
            _singletonService= singletonRandomNumberService;
            _singletonService1=singletonRandomNumberService1;
        }

        public IActionResult Index()
        {
            var transientValue1 = _transientService.GetRandomNumber();
            var transientValue2 = _transientService1.GetRandomNumber();
            var scopedValue1 = _scopedHelperService.GetScopedRandom();
            var scopedValue2 = _scopedHelperService1.GetScopedRandom();
            var singletonValue1 = _singletonService.GetRandomNumber();
            var singletonValue2 = _singletonService1.GetRandomNumber();

            ViewBag.TransientValue1 = transientValue1;
            ViewBag.TransientValue2 = transientValue2;

            ViewBag.ScopedValue1 = scopedValue1;
            ViewBag.ScopedValue2 = scopedValue2;

            ViewBag.SingletonValue1 = singletonValue1;
            ViewBag.SingletonValue2 = singletonValue2;
            return View();
        }

      
    }
}
