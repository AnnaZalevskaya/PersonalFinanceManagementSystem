using Auth.API.Middleware;

namespace Auth.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }
    }
}
