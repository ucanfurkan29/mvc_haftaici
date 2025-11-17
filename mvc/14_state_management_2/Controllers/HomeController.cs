using _13_State_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _13_State_Management.Controllers
{
    public class HomeController : Controller
    {
        // 🏠 ANA SAYFA
        public IActionResult Index()
        {
            // 🔍 Mevcut session ve cookie verilerini oku
            ViewBag.SessionUserName = HttpContext.Session.GetString("UserName");
            ViewBag.CookieTheme = Request.Cookies["Theme"] ?? "Light";
            ViewBag.CookieLanguage = Request.Cookies["Language"] ?? "TR";

            return View();
        }

        // 📋 SESSION DEMO SAYFASI
        public IActionResult Demo()
        {
            // 📊 Session istatistikleri
            ViewBag.SessionCount = HttpContext.Session.GetInt32("VisitCount") ?? 0;
            ViewBag.LastVisit = HttpContext.Session.GetString("LastVisit");

            return View();
        }

        // ✅ SESSION AYARLAMA
        [HttpPost]
        public IActionResult SetSession(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                // 📝 Session'a kullanıcı adını kaydet
                HttpContext.Session.SetString("UserName", userName);

                // 📊 Ziyaret sayısını artır
                int visitCount = HttpContext.Session.GetInt32("VisitCount") ?? 0;
                HttpContext.Session.SetInt32("VisitCount", visitCount + 1);

                // ⏰ Son ziyaret zamanını kaydet
                HttpContext.Session.SetString("LastVisit", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

                TempData["Message"] = $"✅ Session ayarlandı: {userName}";
            }
            else
            {
                TempData["Error"] = "❌ Kullanıcı adı boş olamaz!";
            }

            return RedirectToAction("Index");
        }

        // 🔍 SESSION OKUMA
        public IActionResult GetSession()
        {
            string userName = HttpContext.Session.GetString("UserName") ?? "Bulunamadı";
            int visitCount = HttpContext.Session.GetInt32("VisitCount") ?? 0;

            TempData["Message"] = $"📋 Session Bilgileri - Kullanıcı: {userName}, Ziyaret: {visitCount}";

            return RedirectToAction("Index");
        }

        // 🗑️ SESSION TEMİZLEME
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "🗑️ Tüm session verileri temizlendi!";

            return RedirectToAction("Index");
        }

        // 🍪 COOKIE AYARLAMA
        [HttpPost]
        public IActionResult SetCookie(string theme, string language)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), // 30 gün
                HttpOnly = false, // JavaScript erişimi için
                IsEssential = true
            };

            if (!string.IsNullOrEmpty(theme))
            {
                Response.Cookies.Append("Theme", theme, cookieOptions);
            }

            if (!string.IsNullOrEmpty(language))
            {
                Response.Cookies.Append("Language", language, cookieOptions);
            }

            TempData["Message"] = $"🍪 Cookie ayarlandı - Tema: {theme}, Dil: {language}";

            return RedirectToAction("Index");
        }

        // 🗑️ COOKIE SİLME
        public IActionResult DeleteCookies()
        {
            Response.Cookies.Delete("Theme");
            Response.Cookies.Delete("Language");

            TempData["Message"] = "🗑️ Tüm cookie'ler silindi!";

            return RedirectToAction("Index");
        }
    }
}
