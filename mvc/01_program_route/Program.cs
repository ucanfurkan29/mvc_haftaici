var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "about",
    pattern: "hakkimizda",
    defaults: new { controller = "Home", action = "About"}
    );

app.MapControllerRoute(
    name:"blogDetails",
    pattern:"blog/details/{id}",
    defaults: new {controller = "Blog", action = "Details"},
    constraints: new {id=@"\d+"}//id sadece rakam olmalý
    );

app.Run();
