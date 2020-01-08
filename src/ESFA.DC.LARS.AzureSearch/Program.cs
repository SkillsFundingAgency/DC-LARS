using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ESFA.DC.LARS.AzureSearch
{
    public class Program
    {
        static async Task Main()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = configBuilder.Build();

            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddServiceBus(sbOptions =>
                {
                    sbOptions.MessageHandlerOptions.AutoComplete = true;
                    sbOptions.MessageHandlerOptions.MaxConcurrentCalls = 16;
                });
            }).ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            }).ConfigureServices(services =>
            {
                services.AddAutofac();
            }).ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => configuration).As<IConfigurationRoot>().SingleInstance();
                cb.Register(c => CreateSearchServiceClient(configuration)).As<ISearchServiceClient>().SingleInstance();

                cb.RegisterType<IndexPopulationService>().As<IIndexPopulationService>();
            });

            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }

        // Create the search service client
        private static SearchServiceClient CreateSearchServiceClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }
    }
}