﻿using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Extensions;
using ESFA.DC.LARS.AzureSearch.Interfaces;
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
                var container = ConfigureContainer(configuration, connectionStrings).Build();

                var indexService = container.Resolve<IIndexService>();
                indexService.UpdateIndexes();
            }
            else
            {
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
                }).ConfigureContainer<ContainerBuilder>(c => ConfigureContainer(configuration, connectionStrings));

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
            containerBuilder.Register(c => CreateSearchServiceClient(configuration)).As<ISearchServiceClient>()
                .SingleInstance();

            containerBuilder.RegisterType<IndexPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<IndexService>().As<IIndexService>();

            return containerBuilder;
        }
    }
}