using Microsoft.Extensions.DependencyInjection;

namespace Scrutor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            return services.Scan(
                scan =>
                {
                    scan.FromApplicationDependencies()
                        .AddClasses(classes => classes.Filter(ServiceLifetime.Scoped))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                        .AddClasses(classes => classes.Filter(ServiceLifetime.Transient))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()

                        .AddClasses(classes => classes.Filter(ServiceLifetime.Singleton))
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime();
                }
            );
        }
    }
}
