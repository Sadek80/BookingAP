using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Services;
using BookingAP.Infrastructure.Clock;
using BookingAP.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingAP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
