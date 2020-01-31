using System.Linq;
using Autofac;
using Autofac.Core;
using ESFA.DC.LARS.API.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Xunit;

namespace ESFA.DC.LARS.API.Modules.Tests
{
    public class ModuleTests
    {
        [Fact]
        public void TestRegistrations()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register((c, p) => new TelemetryClient(TelemetryConfiguration.CreateDefault()))
                .As<TelemetryClient>()
                .SingleInstance();

            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<ApiModule>();
            containerBuilder.RegisterModule(new AzureModule
            {
                AzureSettings = new AzureSettings
                {
                    SearchServiceName = "test",
                    SearchServiceAdminApiKey = "testkey"
                }
            });

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
