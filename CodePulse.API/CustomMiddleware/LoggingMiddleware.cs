namespace CodePulse.API.CustomMiddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        // RequestDelegate allows the middleware to invoke the next component in th request pipeline
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Handling request:" + context.Request.Path);

            await _next(context);
            Console.WriteLine("Finished handling request.");
        }
    }
}
