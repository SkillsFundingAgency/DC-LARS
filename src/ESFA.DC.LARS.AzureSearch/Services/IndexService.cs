using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class IndexService : IIndexService
    {
        private readonly IConfiguration _configuration;
        private readonly IIndexDeletionService _indexDeletionService;
        private readonly ConnectionStrings _connectionStrings;
        private readonly IEnumerable<IIndex> _indexes;
        private readonly IEnumerable<IIndexPopulationService> _populationServices;
        private readonly ISearchServiceClient _serviceClient;

        public IndexService(
            ISearchServiceClient serviceClient,
            IConfiguration configuration,
            IIndexDeletionService indexDeletionService,
            ConnectionStrings connectionStrings,
            IEnumerable<IIndex> indexes,
            IEnumerable<IIndexPopulationService> populationServices)
        {
            _configuration = configuration;
            _indexDeletionService = indexDeletionService;
            _connectionStrings = connectionStrings;
            _indexes = indexes;
            _populationServices = populationServices;
            _serviceClient = serviceClient;
        }

        public void UpdateIndexes()
        {
            foreach (var index in _indexes)
            {
                string indexName = _configuration[index.IndexName.ToString()];

                var services = _populationServices
                    .Where(ps => ps.IsMatch(index.IndexName));

                foreach (var service in services)
                {
                    Console.WriteLine("{0}", "Deleting search index...\n");
                    _indexDeletionService.DeleteIndexIfExists(indexName, _serviceClient);

                    Console.WriteLine("{0}", "Creating search index...\n");
                    service.CreateIndex(indexName, _serviceClient);

                    ISearchIndexClient indexClient = _serviceClient.Indexes.GetClient(indexName);
                    Console.WriteLine("{0}", "Uploading search documents...\n");
                    service.PopulateIndex(indexClient, _connectionStrings);
                }
            }
        }
    }
}