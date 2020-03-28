using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Scrutor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(
            this IServiceCollection services,
            Func<Assembly, bool> assemblyFilter = null)
        {
            return services.Scan(
                scan => scan.FromApplicationDependencies(assemblyFilter ?? (_ => true))
                    .RegisterForLifetime(ServiceLifetime.Scoped)
                    .RegisterForLifetime(ServiceLifetime.Singleton)
                    .RegisterForLifetime(ServiceLifetime.Transient)
            );
        }
    }
}
