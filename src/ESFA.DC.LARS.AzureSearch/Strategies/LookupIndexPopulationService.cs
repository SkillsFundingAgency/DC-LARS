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
    public class LookupIndexPopulationService : AbstractPopulationService<LookUpModel>
    {
        private readonly ILarsContextFactory _contextFactory;
        private readonly IAcademicYearService _academicYearService;
        private readonly ISortingService<IssuingAuthorityLookupModel> _issuingAuthoritySortingService;
        private readonly ISortingService<NotionalNVQLevel2LookupModel> _levelSortingService;
        private readonly ISortingService<FrameworkTypeLookupModel> _tLevelTypeSortingService;
        private readonly ISortingService<SectorSubjectAreaTier1LookupModel> _sectorSubjectAreaTier1SortingService;
        private readonly ISortingService<StandardSectorLookupModel> _standardSectorCodeSortingService;
        private readonly ISortingService<AwardingBodyLookupModel> _awardingBodySortingService;

        public LookupIndexPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService,
            ISortingService<IssuingAuthorityLookupModel> issuingAuthoritySortingService,
            ISortingService<NotionalNVQLevel2LookupModel> levelSortingService,
            ISortingService<FrameworkTypeLookupModel> tLevelTypeSortingService,
            ISortingService<SectorSubjectAreaTier1LookupModel> sectorSubjectAreaTier1SortingService,
            ISortingService<StandardSectorLookupModel> standardSectorCodeSortingService,
            ISortingService<AwardingBodyLookupModel> awardingBodySortingService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _academicYearService = academicYearService;
            _issuingAuthoritySortingService = issuingAuthoritySortingService;
            _levelSortingService = levelSortingService;
            _tLevelTypeSortingService = tLevelTypeSortingService;
            _sectorSubjectAreaTier1SortingService = sectorSubjectAreaTier1SortingService;
            _standardSectorCodeSortingService = standardSectorCodeSortingService;
            _awardingBodySortingService = awardingBodySortingService;
        }

        protected override string IndexName => _populationConfiguration.LookupsIndexName;

        public async override Task PopulateIndexAsync()
        {
            var indexClient = GetIndexClient();

            LookUpModel lookups;

            using (var context = _contextFactory.GetLarsContext())
            {
                lookups = new LookUpModel
                {
                    LookUpKey = "1",
                    NotionalNvqLevel2Lookups = await context.LarsNotionalNvqlevelv2Lookups
                        .Select(lvl => new NotionalNVQLevel2LookupModel
                        {
                            NotionalNVQLevelV2 = lvl.NotionalNvqlevelV2,
                            NotionalNVQLevelV2Desc = lvl.NotionalNvqlevelV2desc
                        }).ToListAsync(),
                    AcademicYearLookups = _academicYearService.GetAcademicYears(context)
                        .Select(ay => new AcademicYearLookupModel
                        {
                            IsCurrentAcademicYear = _academicYearService.IsCurrentAcademicYear(ay),
                            AcademicYear = ay.AcademicYear,
                            AcademicYearDesc = ay.AcademicYearDesc
                        }).ToList(),
                    AwardingBodyLookups = await context.LarsAwardOrgCodeLookups
                        .Select(ab => new AwardingBodyLookupModel
                        {
                            AwardingBodyCode = ab.AwardOrgCode,
                            AwardingBodyName = ab.AwardOrgName
                        }).ToListAsync(),
                    ValidityCategoryLookups = await context.LarsValidityCategoryLookups
                        .Select(vc => new ValidityCategoryLookupModel
                        {
                            ValidityCategory = vc.ValidityCategory,
                            ValidityCategoryDescription = vc.ValidityCategoryDesc2
                        }).ToListAsync(),
                    ValidityFundingMappingLookups = await context.LarsValidityFundingMappings
                        .Select(fm => new ValidityFundingMappingLookupModel
                        {
                            ValidityCategory = fm.ValidityCategory,
                            FundingCategory = fm.FundingCategory,
                            EffectiveFrom = fm.EffectiveFrom,
                            EffectiveTo = fm.EffectiveTo
                        }).ToListAsync(),
                    FrameworkTypeLookups = await context.LarsProgTypeLookups
                        .Where(ft => !_tlevelProgTypes.Contains(ft.ProgType))
                        .Select(ft => new FrameworkTypeLookupModel
                        {
                            FrameworkType = ft.ProgType.ToString(),
                            FrameworkTypeDesc = ft.ProgTypeDesc
                        }).ToListAsync(),
                    TLevelTypeLookups = await context.LarsProgTypeLookups
                        .Where(ft => _tlevelProgTypes.Contains(ft.ProgType))
                        .Select(ft => new FrameworkTypeLookupModel
                        {
                            FrameworkType = ft.ProgType.ToString(),
                            FrameworkTypeDesc = ft.ProgTypeDesc
                        }).ToListAsync(),
                    IssuingAuthorityLookups = await context.LarsIssuingAuthorityLookups
                        .Select(ia => new IssuingAuthorityLookupModel
                        {
                            IssuingAuthority = ia.IssuingAuthority.ToString(),
                            IssuingAuthorityDesc = ia.IssuingAuthorityDesc
                        }).ToListAsync(),
                    StandardSectorLookups = await context.LarsStandardSectorCodeLookups
                        .Select(sc => new StandardSectorLookupModel
                        {
                            StandardSectorCode = sc.StandardSectorCode,
                            StandardSectorCodeDesc = sc.StandardSectorCodeDesc2
                        }).ToListAsync(),
                    SectorSubjectAreaTier1Lookups = await context.LarsSectorSubjectAreaTier1Lookups
                        .Select(st => new SectorSubjectAreaTier1LookupModel
                        {
                            SectorSubjectAreaTier1 = st.SectorSubjectAreaTier1.ToString(),
                            SectorSubjectAreaTier1Desc = st.SectorSubjectAreaTier1Desc
                        }).ToListAsync()
                };
            }

            Sort(lookups);

            var indexActions = new List<IndexAction<LookUpModel>> { IndexAction.Upload(lookups) };

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }

        private void Sort(LookUpModel lookups)
        {
            lookups.IssuingAuthorityLookups = _issuingAuthoritySortingService.Sort(lookups.IssuingAuthorityLookups);
            lookups.NotionalNvqLevel2Lookups = _levelSortingService.Sort(lookups.NotionalNvqLevel2Lookups);
            lookups.TLevelTypeLookups = _tLevelTypeSortingService.Sort(lookups.TLevelTypeLookups);
            lookups.SectorSubjectAreaTier1Lookups = _sectorSubjectAreaTier1SortingService.Sort(lookups.SectorSubjectAreaTier1Lookups);
            lookups.StandardSectorLookups = _standardSectorCodeSortingService.Sort(lookups.StandardSectorLookups);
            lookups.AwardingBodyLookups = _awardingBodySortingService.Sort(lookups.AwardingBodyLookups);
        }
    }
}