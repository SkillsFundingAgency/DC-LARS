﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Extensions;
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
            var configFile = "appsettings.json";

            if (args.Any(a => a.Contains("dev")))
            {
                configFile = "appsettings.development.json";
            }

            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile(configFile);
            IConfiguration configuration = configBuilder.Build();

            var populationConfiguration = configuration.GetConfigSection<PopulationConfiguration>();

            if (args.Any(a => a.Contains("run-manual")))
            {
                var start = DateTime.Now;
                Console.WriteLine($"Starting {start}");
                var container = ConfigureContainer(configuration, populationConfiguration).Build();

                var indexService = container.Resolve<IIndexService>();

                await indexService.UpdateIndexesAsync(CancellationToken.None);

                var end = DateTime.Now;
                Console.WriteLine($"Starting {end}");
                var timeTaken = end - start;
                Console.WriteLine($"Time taken: {timeTaken.Minutes} min {timeTaken.Seconds} sec {timeTaken.Milliseconds} ms");

                Console.WriteLine("{0}", "Complete.  Press any key to end application...\n");
                if (args.Any(a => a.Contains("dev")))
                {
                    Console.ReadKey();
                }
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
                    .ConfigureContainer<ContainerBuilder>(c => ConfigureContainer(configuration, populationConfiguration));

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

        private static ContainerBuilder ConfigureContainer(IConfiguration configuration, IPopulationConfiguration populationConfiguration)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register(c => CreateSearchServiceClient(configuration))
                .As<ISearchServiceClient>()
                .SingleInstance();

            containerBuilder.RegisterType<LarsContextFactory>().As<ILarsContextFactory>();

            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();

            containerBuilder.Register(cb => populationConfiguration).As<IPopulationConfiguration>();

            containerBuilder.RegisterType<LookupIndexPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<LearningAimIndexPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<UnitPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<FrameworkPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<StandardsPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<TLevelPopulationService>().As<IIndexPopulationService>();
            containerBuilder.RegisterType<DownloadDataPopulationService>().As<IIndexPopulationService>();

            containerBuilder.RegisterType<IndexService>().As<IIndexService>();

            containerBuilder.RegisterType<AcademicYearService>().As<IAcademicYearService>();
            containerBuilder.RegisterType<ComponentTypeService>().As<IComponentTypeService>();
            containerBuilder.RegisterType<IssuingAuthorityService>().As<IIssuingAuthorityService>();
            containerBuilder.RegisterType<FundingService>().As<IFundingService>();
            containerBuilder.RegisterType<AwardOrgService>().As<IAwardOrgService>();
            containerBuilder.RegisterType<ValidityService>().As<IValidityService>();
            containerBuilder.RegisterType<FrameworkAimService>().As<IFrameworkAimService>();
            containerBuilder.RegisterType<EntitlementCategoryService>().As<IEntitlementCategoryService>();
            containerBuilder.RegisterType<LearningDeliveryCategoryService>().As<ILearningDeliveryCategoryService>();
            containerBuilder.RegisterType<CommonComponentLookupService>().As<ICommonComponentLookupService>();
            containerBuilder.RegisterType<StandardSectorCodeService>().As<IStandardSectorCodeService>();
            containerBuilder.RegisterType<CommonComponentService>().As<ICommonComponentService>();
            containerBuilder.RegisterType<RelatedLearningAimsService>().As<IRelatedLearningAimsService>();
            containerBuilder.RegisterType<DowloadDataProviderService>().As<IDownloadDataProviderService>();

            containerBuilder.RegisterType<NotionalNVQLevel2SortingService>().As<ISortingService<NotionalNVQLevel2LookupModel>>();
            containerBuilder.RegisterType<SectorSubjectAreaTier1SortingService>().As<ISortingService<SectorSubjectAreaTier1LookupModel>>();
            containerBuilder.RegisterType<TLevelTypeSortingService>().As<ISortingService<FrameworkTypeLookupModel>>();
            containerBuilder.RegisterType<AwardingBodySortingService>().As<ISortingService<AwardingBodyLookupModel>>();
            containerBuilder.RegisterType<StandardSectorCodeSortingService>().As<ISortingService<StandardSectorLookupModel>>();
            containerBuilder.RegisterType<IssuingAuthoritySortingService>().As<ISortingService<IssuingAuthorityLookupModel>>();

            return containerBuilder;
        }
    }
}