using _18_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace _18_webapi.DataContext
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {   
        }

        public DbSet<Product> Products { get; set; } //Products tablosunu temsil eden Dbset

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Model oluşturulurken çağrılır
                                                                           // veritabanı şemasını ve ilişkileri yapılandırmak için kullanılır
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Tags) //tags özelliği için özel dönüşüm tanımla
                .HasConversion(
                    v => string.Join(',', v), // Listeyi virgülle ayrılmış bir stringe dönüştür
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Virgülle ayrılmış stringi listeye dönüştür
                );
            modelBuilder.Entity<Product>()
                .Property(p => p.Images) //images özelliği için özel dönüşüm tanımla
                .HasConversion(
                    v => string.Join(',', v), // Listeyi virgülle ayrılmış bir stringe dönüştür
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Virgülle ayrılmış stringi listeye dönüştür
                );

        }

    }
}
