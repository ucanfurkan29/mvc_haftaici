
using _19_AdoNetExample.DbService.Abstract;
using _19_AdoNetExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace _19_AdoNetExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbService _dbService; //veri tabaný iþlemlerini gerçekleþtirmek için servis

        public HomeController(IDbService dbService)
        {
            _dbService = dbService; //Dependency Injection ile IDbService nesnesini alýr
        }
        public IActionResult Index()
        {
            //AddData(); //sayfa yüklendiðinde veri ekleme metodunu çaðýrýr
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddData()
        {
            //veri tabanýna veri eklemek için basit bir sql sorgusu
            string query = "insert into Students (FirstName, LastName, Age) values ('Furkan', 'Uçan', 25)";
            _dbService.ExecuteNonQuery(query); //sorguyu çalýþtýrýr
            return RedirectToAction("Index"); //iþlem sonrasý ana sayfaya yönlendirir
        }

        [HttpPost]
        public IActionResult AddDataSecure([FromForm] Student model)
        {
            string query = "insert into Students (FirstName, LastName, Age) values (@FirstName, @LastName, @Age)";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@FirstName", model.FirstName), //parametre olarak öðrenci adýný ekler
                new SqlParameter("@LastName",model.LastName), //parametre olarak öðrenci soyadýný ekler
                new SqlParameter("@Age", model.Age) //parametre olarak öðrenci yaþýný ekler
            };
            _dbService.ExecuteNonQuery(query, parameters); //parametreli sorguyu çalýþtýrýr
            return RedirectToAction("Index"); //iþlem sonrasý ana sayfaya yönlendirir
        }

        [HttpGet]
        public IActionResult GetData()
        {
            string query = "select Id,FirstName,LastName,Age FROM Students"; //tüm öðrencileri seçen sql sorgusu
            var students = _dbService.ExecuteReader(query); //sorguyu çalýþtýrýr ve sonuçlarý alýr
            return View(students); //sonuçlarý view'a gönderir
        }
        [HttpGet]
        public IActionResult GetCount()
        {
            string query = "select COUNT(*) FROM Students"; //öðrenci sayýsýný dönen sql sorgusu
            var count = _dbService.ExecuteScalar(query); //sorguyu çalýþtýrýr ve sonucu alýr
            return View(count); //sonucu view'a gönderir
        }


    }
}
