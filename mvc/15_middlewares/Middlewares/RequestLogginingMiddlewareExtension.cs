namespace _15_middlewares.Middlewares
{
    public static class RequestLogginingMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestLoggining(this IApplicationBuilder builder)
            //bu metot IApplicationBuilder arayüzünü genişletir ve UseRequestLogginging adında yeni bir uzantı ekler
        {
            return builder.UseMiddleware<RequestLogginMiddleware>(); //RequestLogginMiddleware sınıfını middleware hattına ekliyoruz
            //böylece her http isteği bu hattan geçer
        }

    }
}
