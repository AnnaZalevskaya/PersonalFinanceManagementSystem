using Operations.API.Extensions;

namespace Operations.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .ConfigureMongoDB(builder.Configuration)
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureControllers()
                .ConfigureValidation()
                .ConfigureEndpointsApiExplorer()
                .ConfigureRedis(builder.Configuration)
                .ConfigureRepositoryWrapper()
                .ConfigureMediatR()
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