using Operations.Application.Models.Extensions;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace Operations.API.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = "The requested resource was not found.";
            }
            else if (exception is UnauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "Access is denied.";
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "A server error has occurred.";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(new ErrorDetailsModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
