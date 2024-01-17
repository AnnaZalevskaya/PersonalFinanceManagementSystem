using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;

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
            services.AddOcelot();
            services.AddSwaggerForOcelot(configuration);     
            
            return services;
        }
    }
}
