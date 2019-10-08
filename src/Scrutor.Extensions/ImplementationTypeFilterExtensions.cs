using Microsoft.Extensions.DependencyInjection;
using Scrutor.Extensions.Attributes;
using System;
using System.Reflection;

namespace Scrutor.Extensions
{
    internal static class ImplementationTypeFilterExtensions
    {
        internal static IImplementationTypeFilter Filter(this IImplementationTypeFilter classes, ServiceLifetime lifetime)
            => classes.Where(type => ValidateType(type, lifetime));

        internal static bool ValidateType(Type type, ServiceLifetime lifetime)
        {
            var attributeType = type.GetCustomAttribute<ExportAttribute>()?.Type;

            return attributeType.HasValue ? attributeType == lifetime : false;
        }
    }
}
