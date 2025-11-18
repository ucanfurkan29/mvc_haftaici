using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _17_filter_operation.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)//burada basit bir kullanıcı login oldu mu kontrolü
        {
            var user = context.HttpContext.User; //kullanıcı bilgilerini al
            if (!user.Identity.IsAuthenticated) //eğer kullanıcı doğrulanmamışsa
            {
                context.Result = new RedirectToActionResult("Login","Account",null); //eğer kullanıcı login olmadıysa
                                                                                     //login sayfasına yönlendir
            }
        }
    }
}
