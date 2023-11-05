using BookingAP.Application.Abstractions.Behaviors;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Bookings.Services;
using FluentValidation;
using MediatR;
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


            services.AddSingleton(typeof(IMessagingPipelineBehavior<,>),
                                  typeof(ValidationPipelineBehavior<,>));

            services.AddSingleton(typeof(IMessagingPipelineBehavior<,>),
                                  typeof(LoggingPipelineBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddTransient<BookingPricingService>();

            return services;
        }
    }
}
