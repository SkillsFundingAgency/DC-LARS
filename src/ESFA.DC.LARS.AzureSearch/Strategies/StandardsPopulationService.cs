using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class StandardsPopulationService : AbstractPopulationService<StandardModel>
    {
        private readonly ILarsContextFactory _contextFactory;
        private readonly IStandardSectorCodeService _standardSectorCodeService;
        private readonly IComponentTypeService _componentTypeService;

        public StandardsPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IStandardSectorCodeService standardSectorCodeService,
            IComponentTypeService componentTypeService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _standardSectorCodeService = standardSectorCodeService;
            _componentTypeService = componentTypeService;
        }

        protected override string IndexName => _populationConfiguration.StandardIndexName;

        public async override Task PopulateIndexAsync()
        {
            var indexClient = GetIndexClient();

            IEnumerable<StandardModel> standards;
            IDictionary<string, string> standardSectorCodes;

            using (var context = _contextFactory.GetLarsContext())
            {
                standardSectorCodes = await _standardSectorCodeService.GetStandardSectorCodeDescriptionsAsync(context);
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
            }

            var indexActions = standards.Select(IndexAction.Upload);

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }
    }
}