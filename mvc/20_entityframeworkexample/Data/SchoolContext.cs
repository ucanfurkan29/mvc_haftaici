using _20_entityframeworkexample.Models;
using Microsoft.EntityFrameworkCore;

namespace _20_entityframeworkexample.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) // DI ile yapılandırma
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //model oluşturma işlemlerini yapılandırmak için kullanılır
        {
            // Öğrenci ve Kurs arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<Course>()
                .HasOne(s => s.Student) //Her Course nesnesinin bir Student nesnesi ile ilişkili olduğunu belirtir
                .WithMany(c => c.Courses) //Her Student nesnesinin birden çok Course nesnesi ile ilişkili olabileceğini belirtir
                .HasForeignKey(c => c.StudentId);   //Course tablosundaki StudentId alanının yabancı anahtar olduğunu belirtir
        }

        public DbSet<Student> Students { get; set; } //DbSet, veritabanındaki tabloları temsil eder
        public DbSet<Course> Courses { get; set; } //DbSet, veritabanındaki tabloları temsil eder
    }
}
