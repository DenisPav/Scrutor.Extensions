using Microsoft.Extensions.DependencyInjection;
using Scrutor.Extensions.Attributes;
using System;
using System.Linq;
using Xunit;

namespace Scrutor.Extensions.Tests
{
    public interface IDependency1
    { }

    [Export(ServiceLifetime.Scoped)]
    public class Dependency1 : IDependency1
    { }

    public interface IDependency2
    { }

    [Export(ServiceLifetime.Transient)]
    public class Dependency2 : IDependency2
    { }

    public interface IDependency3
    { }

    [Export(ServiceLifetime.Singleton)]
    public class Dependency3 : IDependency3
    { }

    public interface IDependency4
    { }

    [Export]
    public class Dependency4 : IDependency4 
    { }

    public class NotRegisteredDependency
    {

    }

    public class ServiceCollectionTests : ServiceCollectionTestsBase
    {
        [Theory]
        [InlineData(typeof(IDependency1), typeof(Dependency1), ServiceLifetime.Scoped)]
        [InlineData(typeof(IDependency2), typeof(Dependency2), ServiceLifetime.Transient)]
        [InlineData(typeof(IDependency3), typeof(Dependency3), ServiceLifetime.Singleton)]
        [InlineData(typeof(IDependency4), typeof(Dependency4), ServiceLifetime.Scoped)]
        [InlineData(typeof(Dependency1), typeof(Dependency1), ServiceLifetime.Scoped)]
        [InlineData(typeof(Dependency2), typeof(Dependency2), ServiceLifetime.Transient)]
        [InlineData(typeof(Dependency3), typeof(Dependency3), ServiceLifetime.Singleton)]
        [InlineData(typeof(Dependency4), typeof(Dependency4), ServiceLifetime.Scoped)]
        public void Should_Have_All_Registered_Dependencies(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var services = CreateServiceCollection();
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == serviceType && descriptor.ImplementationType == implementationType && descriptor.Lifetime == lifetime);

            Assert.NotNull(serviceDescriptor);
        }

        [Theory]
        [InlineData(typeof(IDependency1))]
        [InlineData(typeof(Dependency1))]

        public void Should_Throw_When_No_Active_Scope(Type dependencyType)
        {
            var serviceProvider = CreateServiceProvider();

            Assert.Throws<InvalidOperationException>(() => serviceProvider.GetRequiredService(dependencyType));
        }

        [Theory]
        [InlineData(typeof(IDependency1), typeof(Dependency1))]
        [InlineData(typeof(IDependency4), typeof(Dependency4))]
        [InlineData(typeof(Dependency1), typeof(Dependency1))]
        [InlineData(typeof(Dependency4), typeof(Dependency4))]

        public void Should_Resolve_When_Active_Scope_Exists(Type implementationType, Type serviceType)
        {
            var serviceProvider = CreateServiceProvider();
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService(implementationType);

            Assert.NotNull(service);
            Assert.IsType(serviceType, service);
        }

        [Theory]
        [InlineData(typeof(IDependency2), typeof(Dependency2))]
        [InlineData(typeof(IDependency3), typeof(Dependency3))]
        [InlineData(typeof(Dependency2), typeof(Dependency2))]
        [InlineData(typeof(Dependency3), typeof(Dependency3))]
        public void Should_Resolve_Singleton_And_Transient_Services(Type implementationType, Type serviceType)
        {
            var serviceProvider = CreateServiceProvider();
            var service = serviceProvider.GetRequiredService(implementationType);

            Assert.NotNull(service);
            Assert.IsType(serviceType, service);
        }

        [Theory]
        [InlineData(typeof(NotRegisteredDependency))]
        public void Should_Not_Resolve_Unmarked_Service(Type serviceType)
        {
            var serviceProvider = CreateServiceProvider();

            Assert.Throws<InvalidOperationException>(() => serviceProvider.GetRequiredService(serviceType));
        }

        [Fact]
        public void Should_Not_Have_Any_Registration_If_Filtered_By_Assembly()
        {
            var services = CreateServiceCollection(assembly => !assembly.FullName.Contains(nameof(Scrutor.Extensions.Tests)));

            Assert.True(services.Count == 0);
        }
    }
}
