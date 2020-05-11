using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class UnitPopulationService : LearningAimIndexPopulationService
    {
        public UnitPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService,
            IIssuingAuthorityService issuingAuthorityService,
            IComponentTypeService componentTypeService,
            IFundingService fundingService,
            IValidityService validityService,
            ILearningDeliveryCategoryService learningDeliveryCategoryService,
            IFrameworkAimService frameworkAimService,
            IEntitlementCategoryService entitlementCategoryService,
            IAwardOrgService awardOrgService)
            : base(
                  searchServiceClient,
                  populationConfiguration,
                  contextFactory,
                  academicYearService,
                  issuingAuthorityService,
                  componentTypeService,
                  fundingService,
                  validityService,
                  learningDeliveryCategoryService,
                  frameworkAimService,
                  entitlementCategoryService,
                  awardOrgService)
        {
        }

        protected override string IndexName => _populationConfiguration.UnitIndexName;

        public async override Task PopulateIndexAsync()
        {
            await PopulateIndexAsync(true);
        }
    }
}