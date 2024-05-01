using Accounts.BusinessLogic.Exceptions;
using Accounts.BusinessLogic.Models.Extensions;
using Accounts.DataAccess.Exceptions;
using System.Net;

namespace Accounts.Presentation.Middleware
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
                case UserUnauthorizedException:
                    statusCode = HttpStatusCode.Forbidden;
                    message = "The user is not logged in. Access is denied.";
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
                case GetBalanceException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "Error when getting the balance";
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
