﻿using System.Collections.Generic;
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
        private readonly IIssuingAuthoritySortingService _issuingAuthoritySortingService;

        public LookupIndexPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService,
            IIssuingAuthoritySortingService issuingAuthoritySortingService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _academicYearService = academicYearService;
            _issuingAuthoritySortingService = issuingAuthoritySortingService;
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
                        }).ToListAsync()
                };
            }

            lookups.IssuingAuthorityLookups = _issuingAuthoritySortingService.Sort(lookups.IssuingAuthorityLookups.ToList());
            var indexActions = new List<IndexAction<LookUpModel>> { IndexAction.Upload(lookups) };

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }
    }
}