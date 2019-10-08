using Microsoft.Extensions.DependencyInjection;
using Scrutor.Extensions.Attributes;
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

    public class NotRegisteredDependency
    {

    }

    public class ServiceCollectionTests
    {
        [Fact]
        public void Should_Have_All_Registered_Dependencies()
        {
            var services = new ServiceCollection()
                .RegisterDependencies();

            var scopedService = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDependency1) && descriptor.ImplementationType == typeof(Dependency1) && descriptor.Lifetime == ServiceLifetime.Scoped);
            var transientService = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDependency2) && descriptor.ImplementationType == typeof(Dependency2) && descriptor.Lifetime == ServiceLifetime.Transient);
            var singletonService = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDependency3) && descriptor.ImplementationType == typeof(Dependency3) && descriptor.Lifetime == ServiceLifetime.Singleton);

            Assert.NotNull(scopedService);
            Assert.NotNull(transientService);
            Assert.NotNull(singletonService);
        }
    }
}
