using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class StandardsPopulationService : AbstractPopulationService<StandardModel>
    {
        private readonly ILarsContextFactory _contextFactory;
        private readonly IStandardSectorCodeService _standardSectorCodeService;

        public StandardsPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IStandardSectorCodeService standardSectorCodeService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _standardSectorCodeService = standardSectorCodeService;
        }

        protected override string IndexName => _populationConfiguration.StandardIndexName;

        public async override Task PopulateIndexAsync()
        {
            var indexClient = GetIndexClient();

            IEnumerable<StandardModel> standards;

            using (var context = _contextFactory.GetLarsContext())
            {
                var standardSectorCodes = await _standardSectorCodeService.GetStandardSectorCodeDescriptionsAsync(context);
                var commonComponents = await GetCommonComponents(context);

                standards = await context.LarsStandards
                    .Select(st => new StandardModel
                    {
                        StandardCode = st.StandardCode.ToString(),
                        StandardName = st.StandardName,
                        StandardSectorCode = st.StandardSectorCode,
                        StandardSectorCodeDesc2 = standardSectorCodes[st.StandardSectorCode],
                        Version = st.Version,
                        NotionalEndLevel = st.NotionalEndLevel,
                        EffectiveFrom = st.EffectiveFrom,
                        LastDateStarts = st.LastDateStarts,
                        EffectiveTo = st.EffectiveTo,
                        SectorSubjectAreaTier1 = st.SectorSubjectAreaTier1.ToString(),
                        SectorSubjectAreaTier1Desc = st.SectorSubjectAreaTier1Navigation.SectorSubjectAreaTier1Desc2,
                        SectorSubjectAreaTier2 = st.SectorSubjectAreaTier2.ToString(),
                        SectorSubjectAreaTier2Desc = st.SectorSubjectAreaTier2Navigation.SectorSubjectAreaTier2Desc,
                        IntegratedDegreeStandard = st.IntegratedDegreeStandard,
                        OtherBodyApprovalRequired = st.OtherBodyApprovalRequired
                    }).ToListAsync();

                foreach (var standard in standards)
                {
                    standard.CommonComponents = commonComponents[standard.StandardCode].ToList();
                }
            }

            var indexActions = standards.Select(IndexAction.Upload);

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }

        private async Task<ILookup<string, CommonComponentModel>> GetCommonComponents(LarsContext context)
        {
            var results = await context.LarsStandardCommonComponents.Select(c => new CommonComponentModel
            {
                Id = c.StandardCode.ToString(),
                CommonComponent = c.CommonComponent,
                EffectiveFrom = c.EffectiveFrom,
                EffectiveTo = c.EffectiveTo,
                MinLevel = c.MinLevel
            }).ToListAsync();

            return results.ToLookup(c => c.Id, c => c);
        }
    }
}