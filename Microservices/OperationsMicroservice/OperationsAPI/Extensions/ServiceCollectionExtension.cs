using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Operations.Application.Consumers;
using Operations.Application.Interfaces;
using Operations.Application.Interfaces.gRPC;
using Operations.Application.Operations.Queries.GetCategoryDetails;
using Operations.Infrastructure.Data;
using Operations.Infrastructure.Repositories;
using Operations.Infrastructure.Repositories.gRPC;
using Operations.Infrastructure.Settings;

namespace Operations.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureMongoDB(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<OperationsDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureEndpointsApiExplorer(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Account operations API"
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOperationsGrpcRepository, OperationsGrpcRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }


        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
                typeof(GetCategoryDetailsQueryHandler).Assembly));

            return services;
        }

        public static IServiceCollection ConfigureValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            return services;
        }

        public static IServiceCollection ConfigureMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<IMessageConsumer, MessageConsumer>();

            return services;
        }

        public static IServiceCollection ConfigureGrpc(this IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true; 
            });

            return services;
        }
      
        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedMemoryCache();
            services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = configuration.GetSection("Redis:Host").Value;
                options.InstanceName = configuration.GetSection("Redis:Instance").Value;
            });

            return services;
        }
    }
}
