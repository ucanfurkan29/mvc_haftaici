using System.Diagnostics;

namespace _16_middlewares_2.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next=next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            Console.WriteLine("İstek Başlatıldı" + context.Request.Path);

            await _next(context);
            watch.Stop();

            var elapsed = watch.ElapsedMilliseconds;
            if (elapsed > 1000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"YAVAŞ HIZLI: {context.Request.Path} - {elapsed}ms");
            }
            else if (elapsed > 500)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"ORTA HIZLI ISTEK: {context.Request.Path} - {elapsed}ms");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"HIZLI ISTEK: {context.Request.Path} - {elapsed} ms");
            }
            Console.ResetColor();
            context.Response.Headers.Add("X-response-time",$"{elapsed}ms");
        }
    }
}
