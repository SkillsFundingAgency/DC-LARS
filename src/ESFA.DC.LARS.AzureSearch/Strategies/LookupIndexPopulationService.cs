﻿using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class LookupIndexPopulationService : AbstractPopulationService<LookUpModel>
    {
        private readonly ILarsContextFactory _contextFactory;
        private readonly IAcademicYearService _academicYearService;

        public LookupIndexPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _academicYearService = academicYearService;
        }

        protected override string IndexName => _populationConfiguration.LookupsIndexName;

        public override void PopulateIndex()
        {
            var indexClient = GetIndexClient();

            LookUpModel lookups;

            using (var context = _contextFactory.GetLarsContext())
            {
                lookups = new LookUpModel
                {
                    LookUpKey = "1",
                    NotionalNvqLevel2Lookups = context.LarsNotionalNvqlevelv2Lookups
                        .Select(lvl => new NotionalNVQLevel2LookupModel
                        {
                            NotionalNVQLevelV2 = lvl.NotionalNvqlevelV2,
                            NotionalNVQLevelV2Desc = lvl.NotionalNvqlevelV2desc
                        }).ToList(),
                    AcademicYearLookups = _academicYearService.GetAcademicYears(context)
                        .Select(ay => new AcademicYearLookupModel
                        {
                            IsCurrentAcademicYear = _academicYearService.IsCurrentAcademicYear(ay),
                            AcademicYear = ay.AcademicYear,
                            AcademicYearDesc = ay.AcademicYearDesc
                        }).ToList(),
                    AwardingBodyLookups = context.LarsAwardOrgCodeLookups
                        .Select(ab => new AwardingBodyLookupModel
                        {
                            AwardingBodyCode = ab.AwardOrgCode,
                            AwardingBodyName = ab.AwardOrgName
                        }).ToList(),
                    ValidityCategoryLookups = context.LarsValidityCategoryLookups
                        .Select(vc => new ValidityCategoryLookupModel
                        {
                            ValidityCategory = vc.ValidityCategory,
                            ValidityCategoryDescription = vc.ValidityCategoryDesc2
                        }).ToList(),
                    ValidityFundingMappingLookups = context.LarsValidityFundingMappings
                        .Select(fm => new ValidityFundingMappingLookupModel
                        {
                            ValidityCategory = fm.ValidityCategory,
                            FundingCategory = fm.FundingCategory,
                            EffectiveFrom = fm.EffectiveFrom,
                            EffectiveTo = fm.EffectiveTo
                        }).ToList()
                };
            }

            var indexActions = new List<IndexAction<LookUpModel>> { IndexAction.Upload(lookups) };

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }
    }
}