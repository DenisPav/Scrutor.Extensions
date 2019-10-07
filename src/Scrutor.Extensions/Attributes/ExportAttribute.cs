using Microsoft.Extensions.DependencyInjection;
using System;

namespace Scrutor.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExportAttribute : Attribute
    {
        public readonly ServiceLifetime Type;

        public ExportAttribute(ServiceLifetime type)
        {
            Type = type;
        }
    }
}
