using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class LookupIndexPopulationService : IIndexPopulationService
    {
        public bool IsMatch(SearchIndexes index)
        {
            return index == SearchIndexes.LookUpIndex;
        }

        public async Task PopulateIndex(ISearchIndexClient indexClient, ConnectionStrings connectionStrings)
        {
            var config = new DbContextOptionsBuilder<LarsContext>();
            config.UseSqlServer(connectionStrings.LarsConnectionString);

            LookUpModel lookups;
            using (var context = new LarsContext(config.Options))
            {
                lookups = new LookUpModel
                {
                    LookUpKey = "1",
                    NotionalNvqLevel2Lookups = await context.LarsNotionalNvqlevelv2Lookups.Select(lvl =>
                        new NotionalNVQLevel2Model
                        {
                            NotionalNVQLevelV2 = lvl.NotionalNvqlevelV2,
                            NotionalNVQLevelV2Desc = lvl.NotionalNvqlevelV2desc
                        }).ToListAsync()
                };
            }

            var indexActions = new List<IndexAction<LookUpModel>> { IndexAction.Upload(lookups) };

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }

        public void CreateIndex(string indexName, ISearchServiceClient serviceClient)
        {
            var definition = new Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<LookUpModel>()
            };

            serviceClient.Indexes.Create(definition);
        }
    }
}