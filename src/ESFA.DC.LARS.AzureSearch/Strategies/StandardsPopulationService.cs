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
        private readonly ICommonComponentLookupService _commonComponentLookupService;
        private readonly IComponentTypeService _componentTypeService;

        public StandardsPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IStandardSectorCodeService standardSectorCodeService,
            ICommonComponentLookupService commonComponentLookupService,
            IComponentTypeService componentTypeService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _standardSectorCodeService = standardSectorCodeService;
            _commonComponentLookupService = commonComponentLookupService;
            _componentTypeService = componentTypeService;
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
                var commonComponentLookups = await _commonComponentLookupService.GetCommonComponentLookupsAsync(context);
                var componentTypes = await _componentTypeService.GetComponentTypesAsync(context);

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
                        OtherBodyApprovalRequired = st.OtherBodyApprovalRequired,
                        LearningAims = st.LarsStandardAims
                            .Where(sa => sa.LearnAimRefNavigation.LearnAimRefType != UnitLearnAimRefType)
                            .Select(sa => new RelatedLearningAimModel
                            {
                                LearnAimRef = sa.LearnAimRef,
                                LearningAimTitle = sa.LearnAimRefNavigation.LearnAimRefTitle,
                                AwardingBodyCode = sa.LearnAimRefNavigation.AwardOrgCode,
                                Level = sa.LearnAimRefNavigation.NotionalNvqlevelv2,
                                EffectiveFrom = sa.EffectiveFrom,
                                EffectiveTo = sa.EffectiveTo,
                                ComponentType = sa.StandardComponentType,
                                ComponentTypeDesc = sa.StandardComponentType != null ? componentTypes[sa.StandardComponentType.Value] : null
                            }).ToList()
                    }).ToListAsync();

                foreach (var standard in standards)
                {
                    standard.CommonComponents = commonComponents[standard.StandardCode].ToList();
                    standard.CommonComponents.ForEach(c => c.Description = commonComponentLookups.GetValueOrDefault(c.CommonComponent)?.Description);
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
            var results = await context.LarsStandardCommonComponents.Select(c => new
            {
                Id = c.StandardCode.ToString(),
                c.CommonComponent,
                c.EffectiveFrom,
                c.EffectiveTo,
                c.MinLevel
            }).ToListAsync();

            return results.ToLookup(c => c.Id, c => new CommonComponentModel
            {
                CommonComponent = c.CommonComponent,
                EffectiveFrom = c.EffectiveFrom,
                EffectiveTo = c.EffectiveTo,
                MinLevel = c.MinLevel
            });
        }
    }
}