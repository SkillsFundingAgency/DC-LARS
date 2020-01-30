using System.Linq;
using Autofac;
using Autofac.Core;
using Xunit;

namespace ESFA.DC.LARS.API.Modules.Tests
{
    public class ModuleTests
    {
        [Fact]
        public void TestRegistrations()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ApiModule>();
            containerBuilder.RegisterModule<AzureModule>();
            containerBuilder.RegisterModule<LoggingModule>();

            var container = containerBuilder.Build();

            var registeredServices = container.ComponentRegistry.Registrations.SelectMany(x => x.Services)
                .OfType<IServiceWithType>().ToList();

            foreach (var serviceWithType in registeredServices)
            {
                container.Resolve(serviceWithType.ServiceType);
            }
        }
    }
}
