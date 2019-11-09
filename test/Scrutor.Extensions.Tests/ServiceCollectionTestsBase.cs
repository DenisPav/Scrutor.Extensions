using Microsoft.Extensions.DependencyInjection;
using System;

namespace Scrutor.Extensions.Tests
{
    public class ServiceCollectionTestsBase
    {
        public IServiceCollection CreateServiceCollection() => new ServiceCollection()
            .RegisterDependencies();

        public IServiceProvider CreateServiceProvider() => new DefaultServiceProviderFactory(new ServiceProviderOptions
        {
            ValidateScopes = true
        }).CreateServiceProvider(CreateServiceCollection());
    }
}
