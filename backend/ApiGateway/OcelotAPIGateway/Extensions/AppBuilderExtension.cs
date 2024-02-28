using Microsoft.Extensions.Options;
using OcelotAPIGateway.Settings;

namespace OcelotAPIGateway.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseOcelotSwagger(this IApplicationBuilder app, IServiceCollection services)
        {
            var ocelotOptions = services.BuildServiceProvider().GetRequiredService<IOptions<OcelotSwaggerSettings>>();

            app.UseSwagger();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = ocelotOptions.Value.DownstreamSwaggerEndPointBasePath;
                opt.PathToSwaggerGenerator = ocelotOptions.Value.PathToSwaggerGenerator;
            });

            return app;
        }

        public static IApplicationBuilder UseCORSConfiguration(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader());

            return app;
        }
    }
}
