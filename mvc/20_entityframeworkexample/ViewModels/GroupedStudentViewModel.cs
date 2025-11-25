using _20_entityframeworkexample.Models;

namespace _20_entityframeworkexample.ViewModels
{
    public class GroupedStudentViewModel
    {
        public string Department { get; set; } //bölüm adi
        public List<Student> Students { get; set; } //o bölümdeki öğrenciler
    }
}
