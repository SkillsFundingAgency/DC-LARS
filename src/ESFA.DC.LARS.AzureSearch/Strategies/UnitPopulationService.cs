using ESFA.DC.LARS.Azure.Models;
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
            IComponentTypeService componentTypeService)
            : base(searchServiceClient, populationConfiguration, contextFactory, academicYearService, issuingAuthorityService, componentTypeService)
        {
        }

        protected override string IndexName => _populationConfiguration.UnitIndexName;

        public override void PopulateIndex()
        {
            PopulateIndex(true);
        }
    }
}