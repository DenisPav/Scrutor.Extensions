using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Scrutor.Extensions.Tests
{
    public class ServiceCollectionTestsBase
    {
        public IServiceCollection CreateServiceCollection(Func<Assembly, bool> assemblyFilter = null) => new ServiceCollection()
            .RegisterDependencies(assemblyFilter);

        public IServiceProvider CreateServiceProvider() => new DefaultServiceProviderFactory(new ServiceProviderOptions
        {
            ValidateScopes = true
        }).CreateServiceProvider(CreateServiceCollection());
    }
}
