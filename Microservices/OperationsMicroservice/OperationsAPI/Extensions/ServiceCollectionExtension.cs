using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Operations.Api;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Application;
using Operations.Application.Operations.Commands.CreateOperation;
using Operations.Application.Operations.Commands.DeleteOperation;
using Operations.Application.Operations.Queries.GetCategoryDetails;
using Operations.Application.Operations.Queries.GetCategoryList;
using Operations.Application.Operations.Queries.GetCategoryTypeDetails;
using Operations.Application.Operations.Queries.GetCategoryTypeList;
using Operations.Application.Operations.Queries.GetOperationDetails;
using Operations.Application.Operations.Queries.GetOperationList;
using Operations.Infrastructure.Data;
using Operations.Infrastructure.Repositories;
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

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddTransient<IRequestHandler<GetOperationListQuery, List<OperationModel>>, 
                GetOperationListQueryHandler>();
            services.AddTransient<IRequestHandler<GetOperationDetailsQuery, OperationModel>, 
                GetOperationDetailsQueryHandler>();
            services.AddTransient<IRequestHandler<GetOperationListByAccountIdQuery, List<OperationModel>>,
                GetOperationListByAccountIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateOperationCommand>, CreateOperationCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteOperationCommand>, DeleteOperationCommandHandler>();
            services.AddTransient<IRequestHandler<GetCategoryTypeListQuery, List<CategoryTypeModel>>,
                GetCategoryTypeListQueryHandler>();
            services.AddTransient<IRequestHandler<GetCategoryTypeDetailsQuery, CategoryTypeModel>,
                GetCategoryTypeDetailsQueryHandler>();
            services.AddTransient<IRequestHandler<GetCategoryListQuery, List<CategoryModel>>,
                GetCategoryListQueryHandler>();
            services.AddTransient<IRequestHandler<GetCategoryDetailsQuery, CategoryModel>,
                GetCategoryDetailsQueryHandler>();

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
