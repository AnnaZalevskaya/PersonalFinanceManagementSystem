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
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureControllers()
                .ConfigureValidation()
                .ConfigureEndpointsApiExplorer()
                .ConfigureRepositoryWrapper()
                .ConfigureMediatR()
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

            app.MapGrpcService<AccountBalanceGrpcCommandHandler>();

            app.MapControllers();

            app.Run();
        }
    }
}