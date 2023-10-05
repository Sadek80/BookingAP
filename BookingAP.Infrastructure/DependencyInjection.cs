using Bookify.Infrastructure.Data;
using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Data;
using BookingAP.Application.Abstractions.Scheduling;
using BookingAP.Application.Abstractions.Services;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments.Repositories;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Reviews.Repositories;
using BookingAP.Domain.Users.Repositories;
using BookingAP.Infrastructure.Clock;
using BookingAP.Infrastructure.Data;
using BookingAP.Infrastructure.Repositories.Appartments;
using BookingAP.Infrastructure.Repositories.Bookings;
using BookingAP.Infrastructure.Repositories.Reviews;
using BookingAP.Infrastructure.Repositories.Users;
using BookingAP.Infrastructure.Scheduling;
using BookingAP.Infrastructure.Services;
using Dapper;
using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore.Extensions;

namespace BookingAP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            var connectionString = configuration.GetConnectionString("Database") ?? 
                                   throw new ArgumentNullException("Database Connection String is Null");

            services.AddEntityFrameworkMySQL();

            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseMySQL(connectionString,
                                 mySqlOptionsAction: sqlOptions =>
                                 {
                                     sqlOptions.EnableRetryOnFailure(maxRetryCount: 10,
                                     maxRetryDelay: TimeSpan.FromSeconds(30),
                                     errorNumbersToAdd: null);
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
        /// Add Hangifire
        /// </summary>
        /// <param name="services">IServiceCollection to Extend</param>
        /// <returns>Extended IServiceCollection</returns>
        public static IServiceCollection AddHangifireExtension(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException("Database Connection String is Null");

            services.AddHangfire(configurations =>
            {
                configurations.UseSimpleAssemblyNameTypeSerializer()
                              .UseRecommendedSerializerSettings()
                              .UseStorage(
                                new MySqlStorage(
                                    connectionString,
                                    new MySqlStorageOptions
                                    {
                                        QueuePollInterval = TimeSpan.Zero,
                                        JobExpirationCheckInterval = TimeSpan.FromMinutes(30),
                                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                                        PrepareSchemaIfNecessary = true,
                                        DashboardJobListLimit = 10000,
                                        TransactionTimeout = TimeSpan.FromMinutes(1),
                                        TablesPrefix = "Hangfire",
                                    }
                                ));
            });

            services.AddHangfireServer();

            services.AddTransient<IBackgroundJobService, BackgroundJobService>();
            services.AddTransient<ProcessCoreEventJob>();

            return services;
        }
    }
}
