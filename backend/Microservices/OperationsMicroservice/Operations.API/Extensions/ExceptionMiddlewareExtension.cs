using Operations.API.Middleware;

namespace Operations.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }
    }
}
