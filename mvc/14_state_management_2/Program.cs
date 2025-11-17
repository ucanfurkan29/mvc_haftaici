var builder = WebApplication.CreateBuilder(args);

// 🎮 MVC Servisleri
builder.Services.AddControllersWithViews();

// 🧠 SESSION İÇİN GEREKLİ SERVİSLER
builder.Services.AddDistributedMemoryCache(); // Session için bellek cache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika timeout
    options.Cookie.HttpOnly = true; // Güvenlik için
    options.Cookie.IsEssential = true; // GDPR uyumluluğu için
    options.Cookie.Name = "StateManagement.Session"; // Özel session cookie adı
});

var app = builder.Build();

// 🌐 MIDDLEWARE PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ⚠️ ÖNEMLİ: Session middleware'i routing'den sonra, authorization'dan önce!
app.UseSession();

app.UseAuthorization();

// 🛣️ ROUTING CONFIGURATION
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
