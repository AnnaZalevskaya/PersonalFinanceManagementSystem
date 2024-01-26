using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.Services.Implementations;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Data;
using Accounts.DataAccess.UnitOfWork;
using Accounts.Presentation.Consumers;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

            return services;
        }

        public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IFinancialAccountService, FinancialAccountService>();
            services.AddScoped<IFinancialAccountTypeService, FinancialAccountTypeService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IAccountMessageService, AccountMessageService>();

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
            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetAllAccountsConsumer>();
                x.AddConsumer<GetAllAccountsByUserConsumer>();
                x.AddConsumer<GetAccountConsumer>();
                x.AddConsumer<CreateAccountConsumer>();
                x.AddConsumer<UpdateAccountConsumer>();
                x.AddConsumer<DeleteAccountConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost"));
                    cfg.ReceiveEndpoint("accountsQueue", e =>
                    {
                        e.PrefetchCount = 20;
                        e.UseMessageRetry(r => r.Interval(2, 100));

                        e.Consumer<GetAllAccountsConsumer>(context);
                        e.Consumer<GetAllAccountsByUserConsumer>(context);
                        e.Consumer<GetAccountConsumer>(context);
                        e.Consumer<CreateAccountConsumer>(context);
                        e.Consumer<UpdateAccountConsumer>(context);
                        e.Consumer<DeleteAccountConsumer>(context);
                    });
                    cfg.ConfigureNewtonsoftJsonSerializer(settings =>
                    {
                        settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

                        return settings;
                    });
                    cfg.ConfigureNewtonsoftJsonDeserializer(configure =>
                    {
                        configure.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

                        return configure;
                    });
                });
            });
            services.AddMassTransitHostedService();
        
            return services;
        }
    }
}
