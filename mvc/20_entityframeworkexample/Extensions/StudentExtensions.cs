using _20_entityframeworkexample.Models;
using System.Runtime.CompilerServices;

namespace _20_entityframeworkexample.Extensions
{
    public static class StudentExtensions
    {
        public static IDictionary<string,List<Student>> GropByAgeRange(this IEnumerable<Student> students) //Yaş aralığına göre gruplayan extension method
        {
            return students
                .GroupBy(s => 
                {
                    if (s.Age < 18) return "17 yaş ve altı";
                    if (s.Age <25) return "18 ve 24 arası";
                    if (s.Age < 35) return "25 ve 34 arası";
                    return "35 yaş ve üzeri";
                })
                .ToDictionary(g => g.Key, g => g.ToList());

        }
    }
}
