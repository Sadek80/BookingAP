﻿using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Services;
using BookingAP.Infrastructure.Clock;
using BookingAP.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}
