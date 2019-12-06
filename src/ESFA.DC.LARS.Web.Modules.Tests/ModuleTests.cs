using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Services.Clients;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ESFA.DC.LARS.Web.Modules.Tests
{
    public class ModuleTests
    {
        [Fact]
        public void TestRegistrations()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<WebServicesModule>();

            var services = new ServiceCollection();
            services.AddHttpClient<IClientService, ClientService>();

            containerBuilder.Populate(services);

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
