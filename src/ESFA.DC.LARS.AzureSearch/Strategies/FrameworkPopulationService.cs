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
    public class FrameworkPopulationService : AbstractPopulationService<FrameworkModel>
    {
        private readonly ILarsContextFactory _contextFactory;
        private readonly IIssuingAuthorityService _issuingAuthorityService;
        private readonly IComponentTypeService _componentTypeService;
        private readonly ICommonComponentLookupService _commonComponentLookupService;
        private readonly ICommonComponentService _commonComponentService;
        private readonly IRelatedLearningAimsService _relatedLearningAimsService;

        public FrameworkPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IIssuingAuthorityService issuingAuthorityService,
            IComponentTypeService componentTypeService,
            ICommonComponentLookupService commonComponentLookupService,
            ICommonComponentService commonComponentService,
            IRelatedLearningAimsService relatedLearningAimsService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _issuingAuthorityService = issuingAuthorityService;
            _componentTypeService = componentTypeService;
            _commonComponentLookupService = commonComponentLookupService;
            _commonComponentService = commonComponentService;
            _relatedLearningAimsService = relatedLearningAimsService;
        }

        protected override string IndexName => _populationConfiguration.FrameworkIndexName;

        public override async Task PopulateIndexAsync()
        {
            await PopulateIndexAsync(true);
        }

        public async Task PopulateIndexAsync(bool isFramework)
        {
            var indexClient = GetIndexClient();

            IEnumerable<FrameworkModel> frameworks;

            using (var context = _contextFactory.GetLarsContext())
            {
                var issuingAuthorities = await _issuingAuthorityService.GetIssuingAuthoritiesAsync(context);
                var componentTypes = await _componentTypeService.GetComponentTypesAsync(context);
                var commonComponents = await _commonComponentService.GetFrameworkCommonComponents(context);
                var commonComponentLookups = await _commonComponentLookupService.GetCommonComponentLookupsAsync(context);
                var relatedLearningAims = await _relatedLearningAimsService.GetFrameworkRelatedLearningAims(context);

                frameworks = await context.LarsFrameworks
                    .Select(fr => new FrameworkModel
                    {
                        // azure search index must have 1 key field.  Please ensure pattern here is the same as used in common components
                        // and releated learning aim lookups.
                        Id = string.Concat(fr.FworkCode, "-", fr.ProgType, "-", fr.PwayCode),
                        FrameworkCode = fr.FworkCode,
                        ProgramType = fr.ProgType,
                        PathwayCode = fr.PwayCode,
                        PathwayName = fr.PathwayName,
                        ProgramTypeName = fr.ProgTypeNavigation.ProgTypeDesc,
                        SearchableFrameworkCode = fr.FworkCode.ToString(),
                        FrameworkTitle = fr.IssuingAuthorityTitle,
                        EffectiveFrom = fr.EffectiveFrom,
                        EffectiveTo = fr.EffectiveTo,
                        SectorSubjectAreaTier2 = fr.SectorSubjectAreaTier2.ToString(), // decimal not supported by azure
                        SectorSubjectAreaTier2Desc = fr.SectorSubjectAreaTier2Navigation.SectorSubjectAreaTier2Desc,
                        IssuingAuthority = fr.IssuingAuthority,
                        IssuingAuthorityDesc = issuingAuthorities[fr.IssuingAuthority],
                    }).ToListAsync();

                foreach (var framework in frameworks)
                {
                    framework.CommonComponents = commonComponents[framework.Id].ToList();
                    framework.CommonComponents.ForEach(c => c.Description = commonComponentLookups.GetValueOrDefault(c.CommonComponent)?.Description);

                    framework.LearningAims = relatedLearningAims[framework.Id].ToList();
                    framework.LearningAims.ForEach(l => l.ComponentTypeDesc = componentTypes.GetValueOrDefault(l.ComponentType.Value));
                }
            }

            var indexActions = frameworks.Select(IndexAction.Upload);

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }
    }
}