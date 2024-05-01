﻿using Auth.Application.Interfaces;
using Auth.Application.Models.Consts;
using Auth.Application.Producers;
using Auth.Application.Services;
using Auth.Application.Settings;
using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.Settings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Auth.API.Extensions
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
                    Title = "Auth API"
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

        public static IServiceCollection ConfigureSQLServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options
                 => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();

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
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
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

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole<long>>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddUserManager<UserManager<AppUser>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders();

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

            return services;
        }

        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
            services.AddDistributedMemoryCache();
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetSection("Redis:Host").Value;
                options.InstanceName = configuration.GetSection("Redis:Instance").Value;
            });

            return services;
        }
    }
}
