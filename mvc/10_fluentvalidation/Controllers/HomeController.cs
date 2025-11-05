using _10_fluentvalidation.Models;
using _10_fluentvalidation.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace _10_fluentvalidation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValidator<homePageViewModel> _validator;

        public HomeController(IValidator<homePageViewModel> validator)
        {
            _validator = validator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(homePageViewModel model)
        {
            FluentValidation.Results.ValidationResult result = _validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, failure.ErrorMessage);
                }
                return View("Index", model);
            }



            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
