using _10_fluentvalidation.ViewModels;
using FluentValidation;

namespace _10_fluentvalidation.Validators
{
    public class homePageViewModelValidator : AbstractValidator<homePageViewModel>
    {
        public homePageViewModelValidator() 
        {
            RuleFor(vm => vm.KisiNesnesi)
                .NotNull().WithMessage("Kişi nesnesi boş olamaz.");
            RuleFor(vm => vm.AdresNesnesi)
                .NotNull().WithMessage("Adres nesnesi boş olamaz.");

            RuleFor(vm => vm.KisiNesnesi.Ad)
                .NotEmpty().WithMessage("Kişi adı boş olamaz.")
                .NotNull().WithMessage("Kişi adı null olamaz.")
                .Length(3, 50).WithMessage("Kişi adı 3 ile 50 karakter arasında olmalıdır.");

            RuleFor(vm => vm.KisiNesnesi.Soyad)
                .NotEmpty().WithMessage("Kişi soyadı boş olamaz.")
                .NotNull().WithMessage("Kişi soyadı null olamaz.")
                .Length(2, 50).WithMessage("Kişi soyadı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(vm => vm.KisiNesnesi.Yas)
                .GreaterThan(0).WithMessage("Kişi yaşı 0'dan büyük olmalıdır.")
                .LessThan(120).WithMessage("Kişi yaşı 120'den küçük olmalıdır.");

            RuleFor(vm => vm.AdresNesnesi.AdresTanim)
                .NotEmpty().WithMessage("Adres tanımı boş olamaz.")
                .NotNull().WithMessage("Adres tanımı null olamaz.")
                .Length(5, 100).WithMessage("Adres tanımı 5 ile 100 karakter arasında olmalıdır.");

            RuleFor(vm => vm.AdresNesnesi.Sehir)
                .NotEmpty().WithMessage("Şehir boş olamaz.")
                .NotNull().WithMessage("Şehir null olamaz.")
                .Length(2, 50).WithMessage("Şehir 2 ile 50 karakter arasında olmalıdır.");





        }
    }
}
