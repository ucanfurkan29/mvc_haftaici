using _19_AdoNetExample.Models;
using Microsoft.Data.SqlClient;

namespace _19_AdoNetExample.DbService.Abstract
{
    public interface IDbService //veri tabanı servislerini soyutlamak için arayüz
    {
        void ExecuteNonQuery(string query); //INSERT, UPDATE, DELETE işlemleri için
        void ExecuteNonQuery(string query, SqlParameter[] parameters); //parametreli sorgular için
        List<Student> ExecuteReader(string query); //SELECT sorgularını çalıştırır veri setini bir liste olarak döner
        object ExecuteScalar(string query); //tek bir değer döndüren sorgular için
    }
}
