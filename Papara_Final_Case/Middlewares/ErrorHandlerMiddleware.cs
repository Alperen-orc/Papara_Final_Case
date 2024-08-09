using Papara.Schema.Response;
using System.Net;

namespace Papara.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                StatusCode = context.Response.StatusCode
            };

            if (exception is ArgumentNullException)
            {
                errorResponse.Message = "A required argument was null.";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is UnauthorizedAccessException)
            {
                errorResponse.Message = "You are not authorized to perform this action.";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            // Daha fazla özel hata türü ekleyebilirsiniz

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(result);
        }
    }
}
