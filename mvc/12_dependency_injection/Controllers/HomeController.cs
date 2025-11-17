using _12_dependency_injection.Models;
using _12_dependency_injection.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _12_dependency_injection.Controllers
{
    public class HomeController : Controller
    {
        //servisi burada readonly yapýyoruz çünkü dýþarýdan set edilmesini istemiyorum
        private readonly IMyService _myService;

        public HomeController(IMyService myService)
        {
            _myService = myService;//dependency injection burada gerçekleþiyor
        }

        public IActionResult Index()
        {
            List<Student> students = _myService.GetStudents();

            return View(students);
        }

    }
}
