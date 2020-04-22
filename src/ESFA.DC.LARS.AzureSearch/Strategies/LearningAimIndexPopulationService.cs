using System;
using System.Linq;
using System.Threading;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class LearningAimIndexPopulationService : AbstractPopulationService<LearningAimModel>
    {
        private const int PageSize = 2000;
        private readonly ILarsContextFactory _contextFactory;
        private readonly IAcademicYearService _academicYearService;
        private readonly IIssuingAuthorityService _issuingAuthorityService;
        private readonly IComponentTypeService _componentTypeService;

        public LearningAimIndexPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService,
            IIssuingAuthorityService issuingAuthorityService,
            IComponentTypeService componentTypeService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _academicYearService = academicYearService;
            _issuingAuthorityService = issuingAuthorityService;
            _componentTypeService = componentTypeService;
        }

        protected override string IndexName => _populationConfiguration.LearningAimsIndexName;

        public override void PopulateIndex()
        {
            var next = true;
            var indexClient = GetIndexClient();

            using (var context = _contextFactory.GetLarsContext())
            {
                var page = 0;

                var academicYears = _academicYearService.GetAcademicYears(context);

                var issuingAuthorities = _issuingAuthorityService.GetIssuingAuthorities(context);

                var componentTypes = _componentTypeService.GetComponentTypes(context);

                var frameworkAims = context.LarsFrameworkAims
                    .Where(fa => fa.LearnAimRefNavigation.LearnAimRefType != UnitLearnAimRefType)
                    .Select(fa => new LearningAimFrameworkModel
                        {
                            LearnAimRef = fa.LearnAimRef,
                            LearningAimTitle = fa.LearnAimRefNavigation.LearnAimRefTitle,
                            FrameworkTitle = fa.LarsFramework.IssuingAuthorityTitle,
                            FrameworkCode = fa.FworkCode,
                            PathwayCode = fa.PwayCode,
                            ProgramType = fa.ProgType,
                            EffectiveFrom = fa.LarsFramework.EffectiveFrom,
                            EffectiveTo = fa.LarsFramework.EffectiveTo,
                            PathwayName = fa.LarsFramework.PathwayName,
                            ProgramTypeDesc = fa.LarsFramework.ProgTypeNavigation.ProgTypeDesc,
                            IssuingAuthority = fa.LarsFramework.IssuingAuthority,
                            ComponentType = fa.FrameworkComponentType
                        })
                    .AsEnumerable()
                    .GroupBy(gb => gb.LearnAimRef, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(av => av.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);

                var entitlementCategories = context.LarsAnnualValues
                    .Select(av => new EntitlementCategoryModel
                    {
                        LearnAimRef = av.LearnAimRef,
                        EffectiveFrom = av.EffectiveFrom,
                        EffectiveTo = av.EffectiveTo,
                        Category2Description =
                            av.FullLevel2EntitlementCategoryNavigation.FullLevel2EntitlementCategoryDesc,
                        Category3Description =
                            av.FullLevel3EntitlementCategoryNavigation.FullLevel3EntitlementCategoryDesc
                    })
                    .GroupBy(gb => gb.LearnAimRef)
                    .ToDictionary(av => av.Key, em => em.Select(x => x).ToList());

                while (next)
                {
                    var queryStartTime = DateTime.Now;
                    var learningAims = context.LarsLearningDeliveries
                        .Where(ld => ld.LearnAimRefType != UnitLearnAimRefType)
                        .Select(ld => new LearningAimModel
                        {
                            LearnAimRef = ld.LearnAimRef,
                            AwardingBodyCode = ld.AwardOrgCode,
                            AwardingBodyName = context.LarsAwardOrgCodeLookups
                                .Where(l => l.AwardOrgCode == ld.AwardOrgCode)
                                .Select(l => l.AwardOrgName)
                                .FirstOrDefault(),
                            EffectiveFrom = ld.EffectiveFrom,
                            EffectiveTo = ld.EffectiveTo,
                            Level = ld.NotionalNvqlevelv2Navigation.NotionalNvqlevelV2,
                            LevelDescription = ld.NotionalNvqlevelv2Navigation.NotionalNvqlevelV2desc,
                            Type = ld.LearnAimRefTypeNavigation.LearnAimRefTypeDesc,
                            LearningAimTitle = ld.LearnAimRefTitle,
                            GuidedLearningHours = ld.GuidedLearningHours ?? 0,
                            Categories = ld.LarsLearningDeliveryCategories
                                .Select(cat => new CategoryModel
                                {
                                    Reference = cat.CategoryRef,
                                    EffectiveTo = cat.EffectiveTo,
                                    EffectiveFrom = cat.EffectiveFrom,
                                    Title = cat.CategoryRefNavigation.CategoryName,
                                    Description = cat.CategoryRefNavigation.CategoryName,
                                    ParentReference = cat.CategoryRefNavigation.ParentCategoryRef,
                                    ParentDescription = context.LarsCategoryLookups
                                        .Where(l => l.CategoryRef == cat.CategoryRefNavigation.ParentCategoryRef)
                                        .Select(l => l.CategoryName)
                                        .FirstOrDefault()
                                }).ToList(),
                            ValidityModels = ld.LarsValidities
                                .Select(lv => new ValidityModel
                                {
                                    StartDate = lv.StartDate,
                                    EndDate = lv.EndDate,
                                    LastNewStartDate = lv.LastNewStartDate,
                                    ValidityCategory = lv.ValidityCategory.ToUpper(),
                                    ValidityCategoryDescription = lv.ValidityCategoryNavigation.ValidityCategoryDesc2
                                }).ToList(),
                            FundingModels = ld.LarsFundings
                               .Select(lf => new FundingModel
                               {
                                   EffectiveFrom = lf.EffectiveFrom,
                                   EffectiveTo = lf.EffectiveTo,
                                   FundingCategory = lf.FundingCategory,
                                   FundingCategoryDescription = lf.FundingCategoryNavigation.FundingCategoryDesc2,
                                   RateWeighted = lf.RateWeighted.ToString(),
                                   RateUnWeighted = lf.RateUnWeighted.ToString(),
                                   WeightingFactor = lf.WeightingFactor
                               }).ToList()
                        })
                        .OrderBy(ld => ld.LearnAimRef)
                        .ThenBy(ld => ld.EffectiveFrom)
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .ToArray();
                    var queryEndTime = DateTime.Now;

                    var postProcessStartTime = DateTime.Now;
                    foreach (var learningDelivery in learningAims)
                    {
                        entitlementCategories.TryGetValue(learningDelivery.LearnAimRef, out var entitlementCategory);

                        if (frameworkAims.TryGetValue(learningDelivery.LearnAimRef, out var frameworks))
                        {
                            learningDelivery.Frameworks = frameworks;
                            foreach (var framework in learningDelivery.Frameworks)
                            {
                                framework.ComponentTypeDesc = framework.ComponentType != null
                                    ? componentTypes[framework.ComponentType.Value]
                                    : null;

                                framework.IssuingAuthorityDesc = issuingAuthorities[framework.IssuingAuthority];
                            }
                        }

                        learningDelivery.AcademicYears =
                            academicYears.Select(ay =>
                            {
                                var selectedEntitlementCategory = entitlementCategory?
                                    .FirstOrDefault(cat => cat.EffectiveFrom <= ay.EndDate && (cat.EffectiveTo ?? DateTime.MaxValue) >= ay.StartDate);

                                return new AcademicYearModel
                                    {
                                        AcademicYear = ay.AcademicYear,
                                        Validities = learningDelivery.ValidityModels.Where(lv => lv.StartDate <= ay.EndDate && (lv.EndDate ?? DateTime.MaxValue) >= ay.StartDate).ToList(),
                                        Fundings = learningDelivery.FundingModels.Where(lf => lf.EffectiveFrom <= ay.EndDate && (lf.EffectiveTo ?? DateTime.MaxValue) >= ay.StartDate).ToList(),
                                        Level2Category = selectedEntitlementCategory?.Category2Description,
                                        Level3Category = selectedEntitlementCategory?.Category3Description
                                    };
                            }).ToList();

                        learningDelivery.AcademicYears.RemoveAll(ay => !ay.Validities.Any());

                        learningDelivery.ValidityModels = null;
                        learningDelivery.FundingModels = null;
                    }

                    var postProcessEndTime = DateTime.Now;

                    var indexActions = learningAims.Select(IndexAction.Upload);

                    var batch = IndexBatch.New(indexActions);

                    if (batch.Actions.Any())
                    {
                        var startTime = DateTime.Now;
                        indexClient.Documents.Index(batch);
                        var endTime = DateTime.Now;

                        var duration = queryEndTime - queryStartTime;
                        page++;
                        Console.WriteLine($"Processed {page * PageSize} learning aim documents");
                        Console.WriteLine($"query time: {duration.Minutes} mins, {duration.Seconds} secs, {duration.Milliseconds} ms)");

                        duration = postProcessEndTime - postProcessStartTime;
                        Console.WriteLine($"post process time: {duration.Minutes} mins, {duration.Seconds} secs, {duration.Milliseconds} ms)");

                        duration = endTime - startTime;
                        Console.WriteLine($"batch time: {duration.Minutes} mins, {duration.Seconds} secs, {duration.Milliseconds} ms) \n");
                    }
                    else
                    {
                        next = false;
                    }
                }
            }

            Console.WriteLine("Waiting for indexing...\n");
            Thread.Sleep(2000);
        }
    }
}