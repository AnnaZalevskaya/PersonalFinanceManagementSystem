using Auth.Application.Exceptions;
using Auth.Application.Models.Extensions;
using Auth.Core.Exceptions;
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

            switch (exception)
            {
                case NotValidTokenException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "The token isn't valid.";
                    break;
                case DatabaseNotFoundException:
                    statusCode = HttpStatusCode.ServiceUnavailable;
                    message = "Database connection not found.";
                    break;
                case EntityNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "Entity not found";
                    break;
                case EntityAlreadyExistsException:
                    statusCode = HttpStatusCode.Conflict;
                    message = "Entity is already exists";
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Internal server error";
                    break;
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
