namespace _20_entityframeworkexample.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StudentId { get; set; } //Öğrenci kimliği
        public Student Student { get; set; } //navigation property, Course ile ilişkiki Student nesnesine erişim sağlar
    }
}
