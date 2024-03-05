using Operations.API.Extensions;
using Operations.Application.Operations.Commands.gRPC;

namespace Operations.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .ConfigureMongoDB(builder.Configuration)
                .ConfigureRepositoryWrapper()
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureMediatR()
                .ConfigureGrpc()        
                .ConfigureMapperProfiles()
                .ConfigureValidation()
                .ConfigureControllers()
                .ConfigureCORS()
                .ConfigureRedis(builder.Configuration)
                .ConfigureEndpointsApiExplorer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigins");
            app.UseGrpcWeb();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGrpcService<AccountBalanceGrpcCommandHandler>().EnableGrpcWeb();

            app.MapControllers();

            app.Run();
        }
    }
}