using _20_entityframeworkexample.Data;
using _20_entityframeworkexample.Extensions;
using _20_entityframeworkexample.Models;
using _20_entityframeworkexample.ViewModels;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _20_entityframeworkexample.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection ile SchoolContext'i Constractor üzerinden alýyoruz
        //bu sayede tüm actionlarda _context üzerinden veritabaný iþlemlerini yapabiliriz
        private readonly SchoolContext _context;
        public HomeController(SchoolContext context)
        {
            _context = context; //DI gelen contexti alýyoruz ve field'a atýyoruz
        }
        public IActionResult Index()
        {
            List<Student> students = _context.Students.ToList(); //Veritabanýndan tüm öðrencileri çekme
            return View(students);
        }
        public IActionResult Details(int id)
        {
            var student = _context.Students.Find(id); //Veritabanýndan öðrenci bulma

            if (student == null)
            {
                return NotFound(); // 404 öðrenci bulunamadý
            }
            return View(student); //Öðrenci detaylarýný gösterme
        }

        //Öðrenci ekleme formu
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF saldýrýlarýna karþý koruma saðlar
        public IActionResult Create([Bind("Id,Name,Age,Department")] Student student)
        {
            //DataAnnotations ile model doðrulama uygunluðu kontrol ediliyor
            if (ModelState.IsValid)
            {
                _context.Add(student); //Veritabanýna öðrenci ekleme
                _context.SaveChanges(); //Deðiþiklikleri kaydetme
                return RedirectToAction("Index"); //Ana sayfaya yönlendirme
            }
            return View(student); //Model geçerli deðilse formu tekrar göster
        }

        //Edit 
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF saldýrýlarýna karþý koruma saðlar
        public IActionResult Edit(int id, [Bind("Id,Name,Age,Department")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student); //Veritabanýnda öðrenci güncelleme
                    _context.SaveChanges(); //Deðiþiklikleri kaydetme
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; //Hata fýrlat
                    }
                }
                return RedirectToAction("Index"); //Ana sayfaya yönlendirme
            }
            return View(student);
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id); //veritabanýnda öðrenci var mý kontrolü
        }

        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student); //Veritabanýndan öðrenci silme
                _context.SaveChanges(); //Deðiþiklikleri kaydetme
            }
            return RedirectToAction("Index"); //Ana sayfaya yönlendirme
        }

        public IActionResult QuerySyntax()
        {
            var students = (from s in _context.Students
                            where s.Age > 18
                            select s).ToList();
            return View(students);
        }

        public IActionResult MethodSyntax()
        {
            var students = _context.Students
                .Where(s => s.Age < 18)
                .ToList();
            return View(students);
        }

        public IActionResult Join()
        {
            var studentCourses = (from student in _context.Students
                                  join course in _context.Courses on student.Id equals course.StudentId
                                  select new
                                  {
                                      StudentName = student.Name,
                                      CourseTitle = course.Title
                                  }).ToList();
            return View(studentCourses);
        }


        public IActionResult GetStudentsByDepartment(string department)
        {
            var students = new List<Student>();
            try
            {
                //stored procedure çaðrýsý
                students = _context.Students
                    .FromSqlInterpolated($"EXEC GetStudentsByDepartment {department}") //sql injection korumasý için FromSqlInterpolated kullanýyoruz
                    .ToList();
            }
            catch (Exception)
            {
                students = new List<Student>();
            }
            ViewData["Students"] = students;
            return View();
        }

        // Bölüme göre gruplama
        public IActionResult GroupByDepartment()
        {
            //Select depertment, student from Students group by department
            var groupedData = _context.Students.GroupBy(s => s.Department) //Gruplama anahtarýmýz department
                .Select(g => new GroupedStudentViewModel
                {
                    Department = g.Key, //Bölüm adý
                    Students = g.ToList() //Gruptaki öðrenciler
                });

            return View(groupedData);
        }

        [HttpPost("Transaction")]
        public IActionResult AddStudentsWithTransaction([FromBody] List<Student> students)
        {
            using var transaction = _context.Database.BeginTransaction(); //Transaction baþlatma
            //Tüm iþlemler baþarýlý olmalý yoksa hepsi geri alýnacak

            try
            {
                _context.Students.AddRange(students); //Öðrencileri ekleme
                _context.SaveChanges(); //Deðiþiklikleri kaydetme
                transaction.Commit(); //iþlemleri onayla
            }
            catch (Exception)
            {
                transaction.Rollback(); //Hata durumunda iþlemleri geri al
                return BadRequest("Öðrencileri eklerken bir hata oluþtu");
            }
            return Ok("Öðrenciler baþarýyla eklendi");
        }
        public IActionResult RawSql()
        {
            var students = _context.Students
                .FromSqlRaw("SELECT * FROM Students WHERE Age < {0}", 18) //Ham SQL sorgusu
                .ToList();

            return View("Index",students);
        }
        public IActionResult CustomExtensionMethod()
        {
                       var students = _context.Students.ToList();
            var groupedByAgeRange = students.GropByAgeRange(); //Extension method kullanýmý
            return View(groupedByAgeRange);
        }

        //Bulk Insert iþlemi
        //tek seferde birden çok kaydý veritabanýna eklemek için kullanýlýr
        //performans avantajý saðlar
        public IActionResult BulkInsert()
        {
            var students = new List<Student>()
            {
                new Student { Name = "Ali bulk", Age = 20, Department = "Computer Science" },
                new Student { Name = "Ayþe bulk", Age = 22, Department = "Mathematics" },
                new Student { Name = "Mehmet bulk", Age = 19, Department = "Physics" },
                new Student { Name = "Fatma bulk", Age = 21, Department = "Chemistry" }
            };
            _context.BulkInsert(students); //Bulk insert extension method kullanýmý
            return View("Index",students);
        }
    }
}
