
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _14_state_management.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            //Sessison 
            //session stateler uygulamanýn çalýþtýðý süre boyunca verileri saklamamýza yarar.
            //oturum sona erdiðinde otomatik olarak silinir
            //sessionlarda özel bilgilerin saklanmasý önerilmez
            //sessionlar sunucu tabanýnda tutulur.
            HttpContext.Session.SetString("UserName", "Furkan Uçan"); //HttpContext.Session.SetString ile
                                                                      //session oluþturduk ve ekledik.
            ViewBag.UserName = HttpContext.Session.GetString("UserName");//viewbag ile view e gönderdik

            //cookie

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30), //30 dk sonra silinecek
                HttpOnly = true, //sadece HTTP isteklerinde eriþilebilir javascript tarafýndan eriþilemez
                IsEssential = true //çerezin temel iþlevselliði için gerekli olduðunu belirtiriz
            };

            Response.Cookies.Append("UserName", "Furkan Uçan" ,cookieOptions);
            var cookieUserName = Request.Cookies["UserName"]; //Request.Cookies ile cookiere eriþiyoruz
            ViewBag.CookieUserName = cookieUserName;


            return View();
        }

    }
}
