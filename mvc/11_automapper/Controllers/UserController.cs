using _11_automapper.Dto;
using _11_automapper.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _11_automapper.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper; //AutoMapper Nesnesi, nesne dönüşümler için kullanılır
        public UserController(IMapper mapper)
        {
            _mapper = mapper; //Dependency İnjection ile Imapper nesnesi alıcaz
        }
        public IActionResult Index()
        {
            //Örnek User nesnesi
            User user = new User()
            {
                Id = 1,
                FirstName = "Furkan",
                LastName = "Uçan",
                Email = "ucanfurkan29@gmail.com"
            };

            //User => UserDto dönüşümü gerçekleştir.
            //automapper kullanarak user nesnesini userdto nesnesine çevir
            var userDto = _mapper.Map<UserDto>(user);


            return View(userDto);
        }
    }
}
