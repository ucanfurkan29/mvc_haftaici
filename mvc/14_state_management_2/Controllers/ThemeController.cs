using _13_State_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace _13_State_Management.Controllers
{
    public class ThemeController : Controller
    {
        // 🎨 TEMA AYARLAMA (Hem Session hem Cookie)
        public IActionResult SetTheme(string theme)
        {
            // Geçerli tema kontrolü
            if (string.IsNullOrEmpty(theme) || (theme != "light" && theme != "dark"))
            {
                theme = "light"; // Varsayılan
            }

            // 📊 Session'a tema kaydet
            HttpContext.Session.SetString("Theme", theme);

            // 🍪 Cookie'ye de kaydet (kalıcılık için)
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(365), // 1 yıl
                HttpOnly = false, // JavaScript erişimi için
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append("Theme", theme, cookieOptions);

            // 🎯 Başarı mesajı
            var themeText = theme == "dark" ? "🌙 Koyu Tema" : "☀️ Açık Tema";
            TempData["Message"] = $"🎨 {themeText} aktif edildi!";
            TempData["MessageType"] = "success";

            // 🔄 Geri dönüş URL'i kontrol et
            var returnUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // 🔄 TEMA TOGGLE (Geçiş)
        public IActionResult ToggleTheme()
        {
            // Mevcut temayı al
            var currentTheme = HttpContext.Session.GetString("Theme")
                ?? Request.Cookies["Theme"]
                ?? "light";

            // Tersine çevir
            var newTheme = currentTheme == "light" ? "dark" : "light";

            return SetTheme(newTheme);
        }

        // 📱 AJAX ile tema değişimi
        [HttpPost]
        public IActionResult SetThemeAjax([FromBody] ThemeRequest request)
        {
            if (request?.Theme == null)
            {
                return Json(new { success = false, message = "Geçersiz tema" });
            }

            // Tema ayarla
            SetTheme(request.Theme);

            return Json(new
            {
                success = true,
                theme = request.Theme,
                message = $"Tema {request.Theme} olarak değiştirildi"
            });
        }
    }

    // 📝 Request modeli
   
}
