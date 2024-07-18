using Operations.API.Extensions;
using Operations.Application.Operations.Commands.gRPC;
using Operations.Application.Operations.Commands.SignalR;

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
                .ConfigureAuthentication(builder.Configuration)
                .ConfigureAuthorization()
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureMediatR()
                .ConfigureHangfire(builder.Configuration)
                .ConfigureGrpc(builder.Configuration)        
                .ConfigureMapperProfiles()
                .ConfigureValidation()
      //          .ConfigureSignalR()
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

       //     app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigins");
            app.UseGrpcWeb();
        //    app.MapHub<ReportHub>("/reportHub");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGrpcService<AccountBalanceGrpcCommandHandler>().EnableGrpcWeb();

            app.MapControllers();

            app.Run();
        }
    }
}