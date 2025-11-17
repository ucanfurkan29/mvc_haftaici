using Microsoft.AspNetCore.Mvc;

namespace _13_State_Management.Controllers
{
    public class LanguageController : Controller
    {
        // 🌍 DİL AYARLAMA (Cookie ile - kalıcı)
        public IActionResult SetLanguage(string language)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), // 30 gün kalıcı
                HttpOnly = true, // Güvenlik için
                IsEssential = true
            };

            Response.Cookies.Append("Language", language, cookieOptions);

            TempData["Message"] = $"🌍 Dil ayarlandı: {(language == "tr" ? "Türkçe" : "English")}";

            return RedirectToAction("Index", "Home");
        }

        // 🌍 DİL AYARLAMA (Session ile - geçici)
        public IActionResult SetLanguageSession(string language)
        {
            HttpContext.Session.SetString("Language", language);

            TempData["Message"] = $"🌍 Dil session'a ayarlandı: {(language == "tr" ? "Türkçe" : "English")}";

            return RedirectToAction("Index", "Home");
        }
    }
}
