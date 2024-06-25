using Accounts.BusinessLogic.Services.gRPC;
using Accounts.BusinessLogic.Services.SignalR;
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
                .ConfigureRepositoryWrapper()
                .ConfigureAuthentication(builder.Configuration)
                .ConfigureAuthorization()
                .ConfigureSwagger()
                .ConfigureRabbitMQ()
                .ConfigureAppServices()
                .ConfigureGrpc(builder.Configuration)
                .ConfigureMapperProfiles()
                .ConfigureValidation()
     //           .ConfigureSignalR()
                .ConfigureHangfire()
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
            app.UseWebSockets();
            app.UseRouting();

            app.UseCors("AllowSpecificOrigins");
            app.UseGrpcWeb();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGrpcService<PdfReportGrpcService>().EnableGrpcWeb();

            //     app.MapHub<NotificationsHub>("/notifications");
            app.MapControllers();

            app.Run();
        }
    }
}
