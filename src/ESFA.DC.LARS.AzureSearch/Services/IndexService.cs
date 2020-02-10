using System;
using System.Collections.Generic;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class IndexService : IIndexService
    {
        private readonly IEnumerable<IIndexPopulationService> _populationServices;

        public IndexService(IEnumerable<IIndexPopulationService> populationServices)
        {
            _populationServices = populationServices;
        }

        public void UpdateIndexes()
        {
            foreach (var populationService in _populationServices)
            {
                Console.WriteLine("{0}", "Deleting search index...\n");
                populationService.DeleteIndex();

                Console.WriteLine("{0}", "Creating search index...\n");
                populationService.CreateIndex();

                Console.WriteLine("{0}", "Uploading search documents...\n");
                populationService.PopulateIndex();
            }
        }
    }
}