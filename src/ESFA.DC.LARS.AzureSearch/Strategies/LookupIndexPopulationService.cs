using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class LookupIndexPopulationService : AbstractPopulationService<LookUpModel>
    {
        public LookupIndexPopulationService(ISearchServiceClient searchServiceClient, IPopulationConfiguration populationConfiguration)
            : base(searchServiceClient, populationConfiguration)
        {
        }

        protected override string IndexName => _populationConfiguration.LookupsIndexName;

        public override void PopulateIndex()
        {
            var indexClient = GetIndexClient();

            var config = new DbContextOptionsBuilder<LarsContext>();
            config.UseSqlServer(ConnectionString);

            LookUpModel lookups;
            using (var context = new LarsContext(config.Options))
            {
                lookups = new LookUpModel
                {
                    LookUpKey = "1",
                    NotionalNvqLevel2Lookups = context.LarsNotionalNvqlevelv2Lookups.Select(lvl =>
                        new NotionalNVQLevel2Model
                        {
                            NotionalNVQLevelV2 = lvl.NotionalNvqlevelV2,
                            NotionalNVQLevelV2Desc = lvl.NotionalNvqlevelV2desc
                        }).ToList()
                };
            }

            var indexActions = new List<IndexAction<LookUpModel>> { IndexAction.Upload(lookups) };

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }
    }
}