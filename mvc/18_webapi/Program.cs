
using _18_webapi.DataContext;
using _18_webapi.Seeder;
using Microsoft.EntityFrameworkCore;

namespace _18_webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProductContext>(options => //DbContext'i hizmet koleksiyonuna ekle
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add services to the container.
            builder.Services.AddControllers();

            //CORS ayarlarý
            //Cross-Origin Resource Sharing (CORS), bir web uygulamasýnýn farklý bir kaynaktan (alan adý, protokol veya port) kaynaklara eriþmesine izin veren bir güvenlik özelliðidir.
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                    );
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider; //Hizmet saðlayýcýyý al
                var context = services.GetRequiredService<ProductContext>(); //ProductContext hizmetini al
                ProductSeeder.Seed(context); //Veritabanýný tohumla
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors();

            app.Run();
        }
    }
}
