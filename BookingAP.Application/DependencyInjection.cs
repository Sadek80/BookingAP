using BookingAP.Application.Abstractions.Behaviors;
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
            });

            return services;
        }
    }
}
