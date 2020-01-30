using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Extensions;
using ESFA.DC.LARS.AzureSearch.Indexes;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.LARS.AzureSearch.Services;
using ESFA.DC.LARS.AzureSearch.Strategies;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ESFA.DC.LARS.AzureSearch
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfiguration configuration = configBuilder.Build();

            var connectionStrings = configuration.GetConfigSection<ConnectionStrings>();

            if (args.Any(a => a.Contains("run-manual")))
            {
                var start = DateTime.Now;
                Console.WriteLine($"Starting {start}");
                var container = ConfigureContainer(configuration, connectionStrings).Build();

                var indexService = container.Resolve<IIndexService>();

                indexService.UpdateIndexes();

                var end = DateTime.Now;
                Console.WriteLine($"Starting {end}");
                var timeTaken = end - start;
                Console.WriteLine($"Time taken: {timeTaken.Minutes} min {timeTaken.Seconds} sec {timeTaken.Milliseconds} ms");

                Console.WriteLine("{0}", "Complete.  Press any key to end application...\n");
                Console.ReadKey();
            }
            else
            {
                var builder = new HostBuilder();
                builder
                    .ConfigureWebJobs(b =>
                    {
                        b.AddAzureStorageCoreServices();
                        b.AddServiceBus(sbOptions =>
                        {
                            sbOptions.MessageHandlerOptions.AutoComplete = true;
                            sbOptions.MessageHandlerOptions.MaxConcurrentCalls = 16;
                        });
                    })
                    .ConfigureLogging((context, b) => { b.AddConsole(); })
                    .ConfigureServices(services => { services.AddAutofac(); })
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(c => ConfigureContainer(configuration, connectionStrings));

                var host = builder.Build();
                using (host)
                {
                    await host.RunAsync();
                }
            }
        }

        // Create the search service client
        private static SearchServiceClient CreateSearchServiceClient(IConfiguration configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }

        private static ContainerBuilder ConfigureContainer(IConfiguration configuration, ConnectionStrings connectionStrings)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(c => configuration).As<IConfiguration>().SingleInstance();
            containerBuilder.Register(c => connectionStrings).As<ConnectionStrings>().SingleInstance();
            containerBuilder.Register(c => CreateSearchServiceClient(configuration))
                .As<ISearchServiceClient>()
                .SingleInstance();

            containerBuilder.RegisterType<LookupIndexPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<LearningAimIndexPopulationService>().As<IIndexPopulationService>();

            containerBuilder.RegisterType<IndexDeletionService>().As<IIndexDeletionService>();
            containerBuilder.RegisterType<IndexService>().As<IIndexService>();

            containerBuilder.RegisterType<LearningAimIndex>().As<IIndex>();
            containerBuilder.RegisterType<LookupIndex>().As<IIndex>();

            return containerBuilder;
        }
    }
}