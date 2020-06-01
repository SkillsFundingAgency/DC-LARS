using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Extensions;
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
        private readonly ICommonComponentLookupService _commonComponentLookupService;
        private readonly IComponentTypeService _componentTypeService;
        private readonly ICommonComponentService _commonComponentService;
        private readonly IRelatedLearningAimsService _relatedLearningAimsService;

        public StandardsPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IStandardSectorCodeService standardSectorCodeService,
            ICommonComponentLookupService commonComponentLookupService,
            IComponentTypeService componentTypeService,
            ICommonComponentService commonComponentService,
            IRelatedLearningAimsService relatedLearningAimsService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _standardSectorCodeService = standardSectorCodeService;
            _commonComponentLookupService = commonComponentLookupService;
            _componentTypeService = componentTypeService;
            _commonComponentService = commonComponentService;
            _relatedLearningAimsService = relatedLearningAimsService;
        }

        protected override string IndexName => _populationConfiguration.StandardIndexName;

        public async override Task PopulateIndexAsync()
        {
            var indexClient = GetIndexClient();
            IEnumerable<StandardModel> standards;

            using (var context = _contextFactory.GetLarsContext())
            {
                var standardSectorCodes = await _standardSectorCodeService.GetStandardSectorCodeDescriptionsAsync(context);
                var commonComponents = await _commonComponentService.GetStandardCommonComponents(context);
                var relatedLearningAims = await _relatedLearningAimsService.GetStandardRelatedLearningAims(context);
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
                        StandardFundingModels = st.LarsStandardFundings
                            .Select(sf => new StandardFundingModel()
                            {
                                FundingCategoryDescription = sf.FundingCategoryNavigation.FundingCategoryDesc2,
                                BandNumber = sf.BandNumber,
                                EffectiveFrom = sf.EffectiveFrom,
                                EffectiveTo = sf.EffectiveTo,
                                CoreGovContributionCap = sf.CoreGovContributionCap.ToString(),
                                Incentive1618 = sf._1618incentive.ToString(),
                                SmallBusinessIncentive = sf.SmallBusinessIncentive.ToString(),
                                AchievementIncentive = sf.AchievementIncentive.ToString()
                            }).ToList(),
                        StandardApprenticeshipFundingModels = st.LarsApprenticeshipStdFundings
                            .Select(saf => new StandardApprenticeshipFundingModel()
                            {
                                FundingCategoryDescription = saf.FundingCategoryNavigation.FundingCategoryDesc2,
                                BandNumber = saf.BandNumber,
                                EffectiveFrom = saf.EffectiveFrom,
                                EffectiveTo = saf.EffectiveTo,
                                ProviderAdditionalPayment1618 = saf._1618providerAdditionalPayment.ToString(),
                                EmployerAdditionalPayment1618 = saf._1618employerAdditionalPayment.ToString(),
                                FrameworkUplift1618 = saf._1618frameworkUplift.ToString(),
                                CareLeaverAdditionalPayment = saf.CareLeaverAdditionalPayment.ToString(),
                                Duration = saf.Duration.ToString(),
                                MaxEmployerLevyCap = saf.MaxEmployerLevyCap.ToString(),
                                FundableWithoutEmployer = saf.FundableWithoutEmployer
                            }).ToList()
                    }).ToListAsync();

                foreach (var standard in standards)
                {
                    standard.CommonComponents = commonComponents[standard.StandardCode].ToList();
                    standard.CommonComponents.ForEach(c => c.Description = commonComponentLookups.GetValueOrDefault(c.CommonComponent)?.Description);

                    standard.LearningAims = relatedLearningAims[standard.StandardCode].ToList();
                    standard.LearningAims.ForEach(l => l.ComponentTypeDesc = componentTypes.GetValueOrDefault(l.ComponentType.Value));
                }
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