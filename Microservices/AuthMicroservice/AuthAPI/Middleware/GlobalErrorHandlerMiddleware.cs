using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace Auth.API.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "A server error has occurred.";

            if (ex is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound; 
                message = "The requested resource was not found.";
            }
            else if (ex is UnauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized; 
                message = "Access is denied.";
            }

            var response = new { error = message };
            var payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
