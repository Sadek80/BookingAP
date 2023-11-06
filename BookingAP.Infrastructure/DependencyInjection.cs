﻿using Bookify.Infrastructure.Authentication;
using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Data;
using BookingAP.Application.Abstractions.Scheduling;
using BookingAP.Application.Abstractions.Services;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments.Repositories;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Reviews.Repositories;
using BookingAP.Domain.Users.Repositories;
using BookingAP.Infrastructure.Authentication;
using BookingAP.Infrastructure.Authentication.Options;
using BookingAP.Infrastructure.Clock;
using BookingAP.Infrastructure.Data;
using BookingAP.Infrastructure.Data.Helpers;
using BookingAP.Infrastructure.Repositories.Appartments;
using BookingAP.Infrastructure.Repositories.Bookings;
using BookingAP.Infrastructure.Repositories.Reviews;
using BookingAP.Infrastructure.Repositories.Users;
using BookingAP.Infrastructure.Scheduling;
using BookingAP.Infrastructure.Services;
using Dapper;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System.Configuration;

namespace BookingAP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IHostBuilder hostBuilder, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddPersistence(configuration);

            services.AddBackgroundJobs(configuration);

            services.AddLogging(hostBuilder, configuration);

            services.AddMapper();

            services.AddAuthentication(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException("Database Connection String is Null");

            services.AddEntityFrameworkNpgsql();

            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount: 10,
                                                 maxRetryDelay: TimeSpan.FromSeconds(30),
                                                 errorCodesToAdd: null);
                });

                options.UseInternalServiceProvider(serviceProvider);
            });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAppartmentRepository, AppartmentRepository>();

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ =>
                                new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }

        /// <summary>
        /// Add Background Jobs
        /// </summary>
        /// <param name="services">IServiceCollection to Extend</param>
        /// <returns>Extended IServiceCollection</returns>
        private static void AddBackgroundJobs(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException("Database Connection String is Null");

            services.AddHangfire(configurations =>
            {
                configurations.UseSimpleAssemblyNameTypeSerializer()
                              .UseRecommendedSerializerSettings()
                              .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(connectionString));
            });

            services.AddHangfireServer();

            services.AddTransient<IBackgroundJobService, BackgroundJobService>();
            services.AddTransient<ProcessCoreEventJob>();
        }

        /// <summary>
        /// Add Serilog Logging with SEQ
        /// </summary>
        /// <param name="services">IServiceCollection to Extend</param>
        /// <param name="hostBuilder">Host Builder</param>
        /// <param name="configuration">Configuration</param>
        /// <returns></returns>
        private static void AddLogging(this IServiceCollection services, IHostBuilder hostBuilder, IConfiguration configuration)
        {
            hostBuilder.UseSerilog((context, config) =>
            {
                config
                .ReadFrom.Configuration(configuration);
            });
        }

        private static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        }

        private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            });

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
