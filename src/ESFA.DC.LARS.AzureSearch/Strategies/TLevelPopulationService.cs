using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class TLevelPopulationService : FrameworkPopulationService
    {
        public TLevelPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IIssuingAuthorityService issuingAuthorityService,
            IComponentTypeService componentTypeService,
            ICommonComponentLookupService commonComponentLookupService,
            ICommonComponentService commonComponentService,
            IRelatedLearningAimsService relatedLearningAimsService)
            : base(
                  searchServiceClient,
                  populationConfiguration,
                  contextFactory,
                  issuingAuthorityService,
                  componentTypeService,
                  commonComponentLookupService,
                  commonComponentService,
                  relatedLearningAimsService)
        {
        }

        protected override string IndexName => _populationConfiguration.TLevelIndexName;

        public async override Task PopulateIndexAsync(CancellationToken cancellationToken)
        {
            await PopulateIndexAsync(false);
        }
    }
}
