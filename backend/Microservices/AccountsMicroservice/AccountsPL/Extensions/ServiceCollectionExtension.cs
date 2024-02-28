using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.Producers;
using Accounts.BusinessLogic.Services.Implementations;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Data;
using Accounts.DataAccess.Repositories.Implementations;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using FluentValidation.AspNetCore;
using Grpc.Net.Client.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static gRPC.Protos.Client.AccountBalance;

namespace Accounts.Presentation.Extensions
{
    public static class ServiceCollectionExtension
    {
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
                    Title = "Financial accounts API"
                });
            });

            return services;
        }

        public static IServiceCollection ConfigurePostgreSQL(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<FinancialAccountsDbContext>(options
                 => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            return services;
        }

        public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IFinancialAccountService, FinancialAccountService>();
            services.AddScoped<IFinancialAccountTypeService, FinancialAccountTypeService>();
            services.AddScoped<ICurrencyService, CurrencyService>();

            return services;
        }

        public static IServiceCollection ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();

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
            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddSingleton<IMessageConsumer, MessageConsumer>();

            return services;
        }

        public static IServiceCollection ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddGrpcClient<AccountBalanceClient>(options => 
                options.Address = new Uri(configuration.GetSection("GRPC:ServerURI").Value))
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler())); 

            return services;
        }
      
        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
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
