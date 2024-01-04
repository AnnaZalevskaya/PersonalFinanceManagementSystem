using Accounts.BusinessLogic.Services.Implementations;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Data;
using Accounts.DataAccess.UnitOfWork;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
    }
}
