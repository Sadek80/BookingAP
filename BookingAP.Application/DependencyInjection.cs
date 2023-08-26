using BookingAP.Application.Abstractions.Behaviors;
using BookingAP.Domain.Bookings.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookingAP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

                configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddTransient<BookingPricingService>();

            return services;
        }
    }
}
