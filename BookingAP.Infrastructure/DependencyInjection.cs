using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookingAP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));

                options.UseMySQL(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services,
                                                   Assembly[] assemblies)
        {
            services.AddAutoMapper(assemblies);

            return services;
        }
    }
}
