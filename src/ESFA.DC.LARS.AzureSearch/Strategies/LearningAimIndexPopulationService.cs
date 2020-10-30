using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Extensions;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class LearningAimIndexPopulationService : AbstractPopulationService<LearningAimModel>
    {
        private const int PageSize = 2000;
        private const string OFQUALRegulatedQualification = "EOQ";
        private const string OFQUALRegulatedUnit = "EOU";

        private readonly ILarsContextFactory _contextFactory;
        private readonly IAcademicYearService _academicYearService;
        private readonly IIssuingAuthorityService _issuingAuthorityService;
        private readonly IComponentTypeService _componentTypeService;
        private readonly IFundingService _fundingService;
        private readonly IValidityService _validityService;
        private readonly ILearningDeliveryCategoryService _learningDeliveryCategoryService;
        private readonly IFrameworkAimService _frameworkAimService;
        private readonly IEntitlementCategoryService _entitlementCategoryService;
        private readonly IAwardOrgService _awardOrgService;

        public LearningAimIndexPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService,
            IIssuingAuthorityService issuingAuthorityService,
            IComponentTypeService componentTypeService,
            IFundingService fundingService,
            IValidityService validityService,
            ILearningDeliveryCategoryService learningDeliveryCategoryService,
            IFrameworkAimService frameworkAimService,
            IEntitlementCategoryService entitlementCategoryService,
            IAwardOrgService awardOrgService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _academicYearService = academicYearService;
            _issuingAuthorityService = issuingAuthorityService;
            _componentTypeService = componentTypeService;
            _fundingService = fundingService;
            _validityService = validityService;
            _learningDeliveryCategoryService = learningDeliveryCategoryService;
            _frameworkAimService = frameworkAimService;
            _entitlementCategoryService = entitlementCategoryService;
            _awardOrgService = awardOrgService;
        }

        protected override string IndexName => _populationConfiguration.LearningAimsIndexName;

        public override async Task PopulateIndexAsync(CancellationToken cancellationToken)
        {
            await PopulateIndexAsync(false);
        }

        public async Task PopulateIndexAsync(bool units)
        {
            var indexClient = GetIndexClient();

            using (var context = _contextFactory.GetLarsContext())
            {
                // For an unknown reason large datasets with complex joins appear to randomly
                // drop data.  Not sure why but advised approach is to preload data and query
                // that rather than do all in single EF query.
                var academicYears = _academicYearService.GetAcademicYears(context);
                var issuingAuthorities = await _issuingAuthorityService.GetIssuingAuthoritiesAsync(context);
                var componentTypes = await _componentTypeService.GetComponentTypesAsync(context);
                var fundings = await _fundingService.GetFundingsAsync(context);
                var categories = await _learningDeliveryCategoryService.GetLearningDeliveryCategoriesAsync(context);
                var validities = await _validityService.GetValiditiesAsync(context);
                var entitlementCategories = await _entitlementCategoryService.GetEntitlementCategoriesAsync(context);
                var frameworkAims = await _frameworkAimService.GetLearningAimFrameworkAimsAsync(context, units);
                var awardBodyCodes = await _awardOrgService.GetAwardingOrgNamesAsync(context);

                var page = 0;
                var next = true;

                while (next)
                {
                    var queryStartTime = DateTime.Now;
                    var learningAimsQueryable = context.LarsLearningDeliveries.AsQueryable();

                    if (units)
                    {
                        learningAimsQueryable = learningAimsQueryable.Where(ld => ld.LearnAimRefType == UnitLearnAimRefType);
                    }
                    else
                    {
                        learningAimsQueryable = learningAimsQueryable.Where(ld => ld.LearnAimRefType != UnitLearnAimRefType);
                    }

                    var learningAims = learningAimsQueryable
                        .OrderBy(ld => ld.LearnAimRef)
                        .ThenBy(ld => ld.EffectiveFrom)
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .Select(ld => new LearningAimModel
                        {
                            LearnAimRef = ld.LearnAimRef,
                            AwardingBodyCode = ld.AwardOrgCode,
                            EffectiveFrom = ld.EffectiveFrom,
                            EffectiveTo = ld.EffectiveTo,
                            Level = ld.NotionalNvqlevelv2Navigation.NotionalNvqlevelV2,
                            LevelDescription = ld.NotionalNvqlevelv2Navigation.NotionalNvqlevelV2desc,
                            Type = ld.LearnAimRefTypeNavigation.LearnAimRefTypeDesc,
                            LearningAimTitle = ld.LearnAimRefTitle,
                            GuidedLearningHours = ld.GuidedLearningHours.ToString(),
                            IsOFQUAL = ld.LearningDeliveryGenre == OFQUALRegulatedQualification
                                        || ld.LearningDeliveryGenre == OFQUALRegulatedUnit
                        })
                        .ToArray();

                    var queryEndTime = DateTime.Now;

                    var postProcessStartTime = DateTime.Now;

                    foreach (var learningDelivery in learningAims)
                    {
                        PopulateFrameworks(learningDelivery, frameworkAims, issuingAuthorities, componentTypes);
                        PopulateCategories(learningDelivery, categories.GetValueOrDefault(learningDelivery.LearnAimRef, new List<CategoryModel>()));
                        learningDelivery.Categories = categories.GetValueOrDefault(learningDelivery.LearnAimRef, new List<CategoryModel>());
                        learningDelivery.AwardingBodyName = awardBodyCodes.GetValueOrDefault(learningDelivery.AwardingBodyCode);
                        learningDelivery.GuidedLearningHours = GetGuidedLearningHours(learningDelivery.GuidedLearningHours);

                        var fundingForDelivery = fundings.GetValueOrDefault(learningDelivery.LearnAimRef, new List<FundingModel>());
                        var validityForDelivery = validities.GetValueOrDefault(learningDelivery.LearnAimRef, new List<ValidityModel>());
                        var entitlementCategory = entitlementCategories.GetValueOrDefault(learningDelivery.LearnAimRef, new List<EntitlementCategoryModel>());
                        PopulateAcademicYears(learningDelivery, academicYears.ToList(), fundingForDelivery, entitlementCategory, validityForDelivery);
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

        private void PopulateCategories(LearningAimModel learningAim, List<CategoryModel> categories)
        {
            learningAim.Categories = categories;

            foreach (var category in learningAim.Categories.Where(c => string.IsNullOrWhiteSpace(c.ParentDescription)))
            {
                category.ParentDescription = "N/A";
            }
        }

        private string GetGuidedLearningHours(string guidedLearningHours)
        {
            if (string.IsNullOrWhiteSpace(guidedLearningHours))
            {
                return "Not Provided";
            }

            if (guidedLearningHours == "0")
            {
                return "Can be taught fully online";
            }

            return guidedLearningHours;
        }

        private void PopulateFrameworks(LearningAimModel learningAim, IDictionary<string, List<LearningAimFrameworkModel>> frameworkAims, IDictionary<string, string> issuingAuthorities, IDictionary<int, string> componentTypes)
        {
            var frameworks = frameworkAims.GetValueOrDefault(learningAim.LearnAimRef);

            if (frameworks != null)
            {
                learningAim.Frameworks = frameworks;
                foreach (var framework in learningAim.Frameworks)
                {
                    framework.ComponentTypeDesc = framework.ComponentType != null
                        ? componentTypes[framework.ComponentType.Value]
                        : null;

                    framework.IssuingAuthorityDesc = issuingAuthorities[framework.IssuingAuthority];
                }
            }
        }

        private void PopulateAcademicYears(LearningAimModel learningAim, List<LarsAcademicYearLookup> academicYears, List<FundingModel> fundingModels, List<EntitlementCategoryModel> entitlementCategory, List<ValidityModel> validityModels)
        {
            learningAim.AcademicYears =
                academicYears.Select(ay =>
                {
                    var selectedEntitlementCategory = entitlementCategory?
                        .FirstOrDefault(cat => cat.EffectiveFrom <= ay.EndDate && (cat.EffectiveTo ?? DateTime.MaxValue) >= ay.StartDate);

                    return new AcademicYearModel
                    {
                        AcademicYear = ay.AcademicYear,
                        Validities = validityModels.Where(lv => lv.StartDate <= ay.EndDate && (lv.EndDate ?? DateTime.MaxValue) >= ay.StartDate).ToList(),
                        Fundings = fundingModels.Where(lf => lf.EffectiveFrom <= ay.EndDate && (lf.EffectiveTo ?? DateTime.MaxValue) >= ay.StartDate).ToList(),
                        Level2Category = selectedEntitlementCategory?.Category2Description,
                        Level3Category = selectedEntitlementCategory?.Category3Description
                    };
                }).ToList();

            learningAim.AcademicYears.RemoveAll(ay => !ay.Validities.Any());
        }
    }
}