using Accounts.Presentation.Extensions;

namespace Accounts.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .ConfigurePostgreSQL(builder.Configuration)
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureControllers()
                .ConfigureValidation()
                .ConfigureEndpointsApiExplorer()
                .ConfigureRepositoryWrapper()
                .ConfigureAppServices()
                .ConfigureGrpc()
                .ConfigureMapperProfiles();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
