using _17_filter_operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace _17_filter_operation.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        //burada hata loglama veya özel hata ayıklama işlemleri yapabiliriz
        public void OnException(ExceptionContext context) //hata ayıklama metodu
                                                          //uygulamada bir hata oluştuğunda tetiklenir
        {
            context.Result = new ViewResult()//hata sayfasına yönlendirme
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    Model = new ErrorViewModel //hata modeli oluşturme
                                               //hata bilgilerini tutmak için kullanılır
                    {
                        RequestId = context.HttpContext.TraceIdentifier, //istek kimliği
                        ErrorMessage = context.Exception.Message //hata mesajı
                        //hata hakkında daha fazla bilgi de eklenebilir.
                    }
                }
            };

            context.ExceptionHandled = true; //hatanın işlendiğini belirtir
                                             //bu varsayılan hata işleyicisinin devreye girmesini engeller
        }
    }
}
