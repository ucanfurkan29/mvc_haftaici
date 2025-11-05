using _04_viewdata_viewbag_tempdata.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace _04_viewdata_viewbag_tempdata.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ad ="Furkan";

            ArrayList liste = new ArrayList();
            liste.Add("A");
            liste.Add(10);

            ViewBag.liste = liste;

            ViewBag.sonuc = true;

            ViewData["soyad"] = "Ucan";

            TempData["cinsiyet"] = "Erkek";

            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.text = ViewBag.ad; //null deðer döner. viewbag ve viewdata iki action arasýnda sonuc taþýmaz.

            TempData["c"] = TempData["cinsiyet"];

            return View();
        }
    }
}

// ====================================================================
// ÖZET: VERÝ TAÞIMA YÖNTEMLERÝ KARÞILAÞTIRMASI
// ====================================================================
//
// | Özellik        | ViewBag        | ViewData       | TempData       |
// |----------------|----------------|----------------|----------------|
// | Tip            | Dynamic        | Dictionary     | Dictionary     |
// | Ömür           | 1 request      | 1 request      | 2 request      |
// | Syntax         | Kolay          | Key-value      | Key-value      |
// | Casting        | Gerektirmez    | Gerekebilir    | Gerekebilir    |
// | Redirect sonrasý| Kaybolur      | Kaybolur       | Kalýr          |
// | Saklama        | Request        | Request        | Session        |
//
// KULLANIM ÖNERÝLERÝ:
// ? Ayný view içinde basit veri: ViewBag (daha kolay)
// ? Redirect sonrasý mesaj: TempData (örn: "Kayýt baþarýlý!")
// ? Karmaþýk veri yapýlarý: Strongly-typed Model (en iyi)
// ? ViewData: ViewBag'den avantajý yok, eski syntax