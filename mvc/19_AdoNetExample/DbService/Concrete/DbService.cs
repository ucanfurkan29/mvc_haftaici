using _19_AdoNetExample.DbService.Abstract;
using _19_AdoNetExample.Models;
using Microsoft.Data.SqlClient;

namespace _19_AdoNetExample.DbService.Concrete
{
    public class DbService : IDbService
    {
        private readonly string _connectionString; //Bağlantı dizesini saklar
        public DbService(IConfiguration configuration) //Bağlantı dizesini yapılandırmadan alır
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection"); //appsettings.json dosyasındaki bağlantı dizesi
        }

        //Sql sorgularını çalıştırmak için metodlar (INSERT, UPDATE,DELETE)
        public void ExecuteNonQuery(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();//Bağlantıyı açar
                    command.ExecuteNonQuery(); //Komutu çalıştırır, etkilenen satır sayısını döner
                }
            }
        }

        //parametreli sorgular için metod
        //sql injection saldırılarını önlemek için kullanılır
        //kullanıcı girdilerini doğrudan sorguya eklemek yerine parametreler kullanılır
        //bu sayede kullanıcı giridleirni sql sorgusunun bir parçası olarak değil, veri olarak işlenir
        //böylece kötü niyetli kodların çalıştırılması engellenir
        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters); //parametreler varsa komuta eklenir
                    }

                    connection.Open(); //bağlantıyı açar
                    command.ExecuteNonQuery(); //komutu çalıştırır
                }
            }
        }

        //SELECT sorgularını çalıştırır ve sonuçları bir liste olarak döner
        public List<Student> ExecuteReader(string query)
        {
            var results = new List<Student>(); //sonuçları saklamak için liste

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open(); //bağlantıyı açar
                    using (var reader = command.ExecuteReader()) //veri okuyucu oluşturur
                    {
                        while (reader.Read()) //her satır için
                        {
                            var student = new Student //yeni öğrenci nesnesi oluşturur
                            {
                                Id = Convert.ToInt32(reader["Id"]), //Id sütununu oku
                                FirstName = reader["FirstName"].ToString(), //FirstName sütununu oku
                                LastName = reader["LastName"].ToString(), //LastName sütununu oku
                                Age = Convert.ToInt32(reader["Age"]) //Age sütununu oku
                            };
                            results.Add(student); //listeye ekler
                        }
                    }
                }
            }
            return results; //sonuç listesini döner
        }

        //tek bir değer döndüren sorgular için metod (count, sum, max, min gibi)
        public object ExecuteScalar(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open(); //bağlantıyı açar
                    return command.ExecuteScalar(); //tek bir değer döner
                }
            }
        }
    }
}
