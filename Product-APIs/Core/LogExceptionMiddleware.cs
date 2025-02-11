

using System.Net;

namespace Product_APIs.Core
{
    public class LogExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<LogExceptionMiddleware> _logger;

        public LogExceptionMiddleware(ILogger<LogExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
