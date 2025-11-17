using _13_State_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _13_State_Management.Controllers
{
    public class ShoppingCartController : Controller
    {
        // 🛒 SEPET GÖRÜNTÜLEME
        public IActionResult Index()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }
        public IActionResult ViewCart()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }

        // ➕ SEPETE ÜRÜN EKLEME
        public IActionResult AddToCart(int id, string name, decimal price)
        {
            var cartItems = GetCartItems();

            // Aynı üründen varsa miktarını artır
            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                // Yeni ürün ekle
                cartItems.Add(new CartItem
                {
                    ProductId = id,
                    ProductName = name,
                    Price = price,
                    Quantity = 1
                });
            }

            SaveCartItems(cartItems);
            TempData["Message"] = $"✅ {name} sepete eklendi!";

            return RedirectToAction("Index");
        }

        // ➖ SEPETTEN ÜRÜN ÇIKARMA
        public IActionResult RemoveFromCart(int id)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItems(cartItems);
                TempData["Message"] = $"🗑️ {item.ProductName} sepetten çıkarıldı!";
            }

            return RedirectToAction("Index");
        }

        // 🗑️ SEPETİ TEMİZLE
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            TempData["Message"] = "🗑️ Sepet temizlendi!";

            return RedirectToAction("Index");
        }

        // 📋 SEPET VERİLERİNİ OKUMA
        private List<CartItem> GetCartItems()
        {
            // 🔍 Session'dan JSON string'i al
            var cartJson = HttpContext.Session.GetString("Cart");

            // 🤔 NEDEN BU KONTROL VAR?
            // Session'da "Cart" anahtarı yoksa null döner
            // Boş string de olabilir, bu durumda da liste oluşturmamız gerekir
            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>() // 📝 Boşsa yeni boş liste oluştur
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
            // 🔄 JSON'u List<CartItem>'a çevir, başarısızsa boş liste döndür
            // ?? operatörü: Deserialization null dönerse boş liste kullan
        }

        // 💾 SEPET VERİLERİNİ KAYDETME
        private void SaveCartItems(List<CartItem> cartItems)
        {
            // 🔄 List<CartItem> nesnesini JSON string'e çevir
            // NEDEN JSON KULLANIYORUZ?
            // - Session sadece string, int, byte[] tiplerini destekler
            // - Karmaşık nesneleri (List, Class) saklamak için JSON'a çevirmek gerekir
            var cartJson = JsonConvert.SerializeObject(cartItems);

            // 💾 JSON string'i session'a kaydet
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}
