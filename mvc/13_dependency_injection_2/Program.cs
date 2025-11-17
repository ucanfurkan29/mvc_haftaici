using _12_Dependency_Injection_2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<SingletonRandomNumberService>();// Servis kaydý container a ekleniyor
builder.Services.AddTransient<TransientRandomNumberService>();// Servis kaydý container a ekleniyor
builder.Services.AddScoped<ScopedRandomNumberService>();// Servis kaydý container a ekleniyor
builder.Services.AddScoped<ScopedHelperService>();// Servis kaydý container a ekleniyor
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
