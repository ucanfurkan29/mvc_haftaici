namespace _19_AdoNetExample.Models
{
    public class Student //Db yapısı için model sınıfı
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
