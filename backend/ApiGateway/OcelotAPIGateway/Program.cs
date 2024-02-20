using Ocelot.Middleware;
using OcelotAPIGateway.Extensions;

namespace OcelotAPIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("Configuration/ocelot.json");

            builder.Services
                .ConfigureOcelot(builder.Configuration)
                .ConfigureEndpointsApiExplorer();

            var app = builder.Build();

            app.UsePathBase("/gateway");

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
