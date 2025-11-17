using _12_dependency_injection.Models;
using _12_dependency_injection.Services.Abstract;

namespace _12_dependency_injection.Services.Concrete
{
    public class MyService : IMyService
    {
        public List<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student{Id = 1, Name = "Ali", Age = 20},
                new Student{Id = 2, Name = "Ayşe", Age = 21},
                new Student{Id = 3, Name = "Mehmet", Age = 19}
            };
        }
    }
}
