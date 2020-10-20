using System.Linq;
using Autofac;
using Autofac.Core;
using ESFA.DC.LARS.Web.Configuration;
using ESFA.DC.LARS.Web.Interfaces;
using Xunit;

namespace ESFA.DC.LARS.Web.Modules.Tests
{
    public class ModuleTests
    {
        [Fact]
        public void TestRegistrations()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<ApiSettings>().As<IApiSettings>();
            containerBuilder.RegisterModule<WebServicesModule>();

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
