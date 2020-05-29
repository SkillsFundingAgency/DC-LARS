﻿using System;
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
    public class FrameworkPopulationService : AbstractPopulationService<FrameworkModel>
    {
        private readonly ILarsContextFactory _contextFactory;
        private readonly IIssuingAuthorityService _issuingAuthorityService;
        private readonly IComponentTypeService _componentTypeService;
        private readonly ICommonComponentLookupService _commonComponentLookupService;

        public FrameworkPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IIssuingAuthorityService issuingAuthorityService,
            IComponentTypeService componentTypeService,
            ICommonComponentLookupService commonComponentLookupService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _issuingAuthorityService = issuingAuthorityService;
            _componentTypeService = componentTypeService;
            _commonComponentLookupService = commonComponentLookupService;
        }

        protected override string IndexName => _populationConfiguration.FrameworkIndexName;

        public async override Task PopulateIndexAsync()
        {
            var indexClient = GetIndexClient();

            IEnumerable<FrameworkModel> frameworks;

            using (var context = _contextFactory.GetLarsContext())
            {
                var issuingAuthorities = await _issuingAuthorityService.GetIssuingAuthoritiesAsync(context);
                var componentTypes = await _componentTypeService.GetComponentTypesAsync(context);
                var commonComponents = await GetCommonComponents(context);
                var commonComponentLookups = await _commonComponentLookupService.GetCommonComponentLookupsAsync(context);

                frameworks = await context.LarsFrameworks
                    .Select(fr => new FrameworkModel
                    {
                        // azure search index must have 1 key field.  Please ensure pattern here is the same as used in CreateFrameworkId
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
                        LearningAims = fr.LarsFrameworkAims
                            .Where(fa => fa.LearnAimRefNavigation.LearnAimRefType != UnitLearnAimRefType)
                            .Select(fa => new FrameworkAimModel
                            {
                                LearnAimRef = fa.LearnAimRef,
                                LearningAimTitle = fa.LearnAimRefNavigation.LearnAimRefTitle,
                                AwardingBodyCode = fa.LearnAimRefNavigation.AwardOrgCode,
                                Level = fa.LearnAimRefNavigation.NotionalNvqlevelv2,
                                EffectiveFrom = fa.EffectiveFrom,
                                EffectiveTo = fa.EffectiveTo,
                                ComponentType = fa.FrameworkComponentType,
                                ComponentTypeDesc = fa.FrameworkComponentType != null ? componentTypes[fa.FrameworkComponentType.Value] : null
                            }).ToList()
                    }).ToListAsync();

                foreach (var framework in frameworks)
                {
                    framework.CommonComponents = commonComponents[framework.Id].ToList();
                    framework.CommonComponents.ForEach(c => c.Description = commonComponentLookups.GetValueOrDefault(c.CommonComponent)?.Description);
                }
            }

            var indexActions = frameworks.Select(IndexAction.Upload);

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }

        private async Task<ILookup<string, CommonComponentModel>> GetCommonComponents(LarsContext context)
        {
            var results = await context.LarsFrameworkCmnComps.Select(c => new
            {
                // Please note this must match the FrameworkID generated on initial population
                Id = string.Concat(c.FworkCode, "-", c.ProgType, "-", c.PwayCode),
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