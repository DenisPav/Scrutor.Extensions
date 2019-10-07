using Microsoft.Extensions.DependencyInjection;
using Scrutor.Extensions.Attributes;
using System;
using System.Reflection;

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
                        .AddClasses(classes => classes.Where(CreateFilter(ServiceLifetime.Scoped)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                        .AddClasses(classes => classes.Where(CreateFilter(ServiceLifetime.Transient)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()

                        .AddClasses(classes => classes.Where(CreateFilter(ServiceLifetime.Singleton)))
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime();
                }
            );
        }

        internal static Func<Type, bool> CreateFilter(ServiceLifetime lifetime)
        {
            return type =>
            {
                var attributeType = type.GetCustomAttribute<ExportAttribute>()?.Type;

                return attributeType.HasValue ? attributeType == lifetime : false;
            };
        }
    }
}
