using _19_AdoNetExample.DbService.Abstract;
using _19_AdoNetExample.DbService.Concrete;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDbService,DbService>(); //DbService'i baðýmlýlýk olarak ekle
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); //appsettings.json dosyasýný yapýlandýrmaya ekle

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
