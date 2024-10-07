using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Producers;
using Accounts.BusinessLogic.Services.Implementations;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.BusinessLogic.Services.SignalR;
using Accounts.BusinessLogic.Settings;
using Accounts.DataAccess.Dapper.Data;
using Accounts.DataAccess.Dapper.Repositories.Implementations;
using Accounts.DataAccess.Dapper.Repositories.Interfaces;
using Accounts.DataAccess.Dapper.Settings;
using Accounts.DataAccess.Data;
using Accounts.DataAccess.Repositories.Implementations;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using FluentValidation.AspNetCore;
using Grpc.Net.Client.Web;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>();

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Value.Issuer,
                        ValidAudience = jwtOptions.Value.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                    };
                });

            return services;
        }

        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicyConsts.ClientOnly, policy =>
                {
                    policy.RequireRole(RoleConsts.Client);
                });

                options.AddPolicy(AuthPolicyConsts.AdminOnly, policy =>
                {
                    policy.RequireRole(RoleConsts.Admin);
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureORM(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            var connStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStrings>>();

            services.AddSingleton<DapperContext>();
            services.AddDbContext<FinancialAccountsDbContext>(optionsBuilder => 
                optionsBuilder.UseNpgsql(connStrings.Value.DefaultConnection, options => 
                    options.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: ["4060"]
            )));

            return services;
        }

        public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountStatisticsRepository, AccountStatisticsRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            return services;
        }

        public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IFinancialAccountService, FinancialAccountService>();
            services.AddScoped<IFinancialAccountTypeService, FinancialAccountTypeService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IAccountPdfReportService, AccountPdfReportService>();
            services.AddScoped<IFinancialGoalService, FinancialGoalService>();
            services.AddScoped<IFinancialGoalTypeService, FinancialGoalTypeService>();
            services.AddScoped<IAccountStatisticsService, AccountStatisticsService>();

            return services;
        }

        public static IServiceCollection ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy => policy.WithOrigins("http://localhost:4200")
                        .AllowCredentials()
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

        public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            var rabbitMQOptions = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMQSettings>>();

            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddSingleton<IMessageConsumer, MessageConsumer>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitMQOptions.Value.Uri)); 
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }

        public static IServiceCollection ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

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

        public static IServiceCollection ConfigureSignalR(this IServiceCollection services)
        {
            services.AddSignalR(options => {
                options.EnableDetailedErrors = true;
            });
            services.AddHostedService<NotificationService>();

            return services;
        }

        public static IServiceCollection ConfigureHangfire(this IServiceCollection services)
        {
            var connStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStrings>>();

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(connStrings.Value.HangfireConnection));
            services.AddHangfireServer();

            return services;
        }
    }
}
