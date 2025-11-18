namespace _15_middlewares.Middlewares
{
    public class RequestLogginMiddleware
    {
        private readonly RequestDelegate _next; //RequestDelegate, bir sonraki middleware bileşenini temsil eder
        public RequestLogginMiddleware(RequestDelegate next)//yapıcı metot ile requestLogginMiddelware sınıfının örneğinde çağırır
        {
            _next = next;//sonraki middleware bileşenini saklar, böylece istek işleme hattında ilerleyebilir.

        }
        public async Task InvokeAsync(HttpContext context) 
        {
            var message = $"Middleware Çalıştı: {context.Request.Path}"; //gelen isteğin yolunu alır ve ekrana mesaj oluştur
            context.Items["MiddlewareMessage"] = message;

            await _next(context); //sonrki middleware bileşenini çağırır ve Http isteği iletir
            //await ifadesi işlemin tamamlanmasını bekler ve asenkron programlamayyı destekler


            Console.WriteLine("Yanıt Gönderildi: " + context.Response.StatusCode); //yanıt durumunu konsola yazdırdık

        }
    }
}
