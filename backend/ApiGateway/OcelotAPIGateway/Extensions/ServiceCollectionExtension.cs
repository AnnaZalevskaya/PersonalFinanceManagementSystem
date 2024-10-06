using Ocelot.DependencyInjection;
using OcelotAPIGateway.Settings;

namespace OcelotAPIGateway.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureEndpointsApiExplorer(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            return services;
        }

        public static IServiceCollection ConfigureOcelot(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OcelotSwaggerSettings>(configuration.GetSection(nameof(OcelotSwaggerSettings)));
            services.AddOcelot();
            services.AddSwaggerForOcelot(configuration);     
            
            return services;
        }

        public static IServiceCollection ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors();

            return services;
        }
    }
}
