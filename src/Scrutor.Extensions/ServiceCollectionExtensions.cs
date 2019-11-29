using Microsoft.Extensions.DependencyInjection;

namespace Scrutor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            return services.Scan(
                scan => scan.FromApplicationDependencies()
                    .RegisterForLifetime(ServiceLifetime.Scoped)
                    .RegisterForLifetime(ServiceLifetime.Singleton)
                    .RegisterForLifetime(ServiceLifetime.Transient)
            );
        }
    }
}
