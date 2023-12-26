using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth.API
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureEndpointsApiExplorer(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            return services;
        }

        public static IServiceCollection ConfigureSQLServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options
                 => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static void Configure(IWebHostEnvironment env,  AuthDbContext context)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
    }
}
