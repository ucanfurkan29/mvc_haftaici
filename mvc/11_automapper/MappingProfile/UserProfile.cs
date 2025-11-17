using _11_automapper.Models;
using AutoMapper;
using _11_automapper.Dto;
namespace _11_automapper.MappingProfile
{
    public class UserProfile : Profile
    {

        //User => UserDto dönüşümünü tanımlıyoruz
        //burada firstName ve lastnamei fullname e mapliyoruz
        public UserProfile()
        {
            // Hedefteki FullName özelliğini, kaynaktaki FirstName ve LastName değerlerini birleştirerek doldur diyoruz
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, //ForMember ile Fullname için özel eşleşme kuralı tanımlıyoruz
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}" //MapFrom Bu propertynin değerinin nereden
                                                                            //ve nasıl geleceğini söylüyoruz
            ));

            //UserDto => User dönüşüm tanımlıyoruz 
            //iki taraflı dönüşüm olabilir
            CreateMap<UserDto, User>();
        }
    }
}
