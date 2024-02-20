using Auth.API.Extensions;
using Auth.API.Middleware;

namespace Auth.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services               
                .ConfigureSQLServer(builder.Configuration)
                .ConfigureRepositoryWrapper()
                .ConfigureAuthentication()
                .ConfigureAuthorization()
                .ConfigureIdentity()
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureAppServices(builder.Configuration)
                .ConfigureMapperProfiles()               
                .ConfigureValidation()
                .ConfigureControllers()
                .ConfigureCORS()
                .ConfigureRedis(builder.Configuration)
                .ConfigureEndpointsApiExplorer();

            var app = builder.Build();

            app.MigrateDatabase();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}