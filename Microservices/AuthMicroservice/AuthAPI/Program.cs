using Auth.API.Extensions;

namespace Auth.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .ConfigureSQLServer(builder.Configuration)
                .ConfigureControllers()
                .ConfigureValidation()
                .ConfigureEndpointsApiExplorer()
                .AddSwaggerGen()
                .ConfigureRepositoryWrapper()
                .ConfigureAuth(builder.Configuration)
                .ConfigureMapperProfiles();

            var app = builder.Build();

            app.MigrateDatabase();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}