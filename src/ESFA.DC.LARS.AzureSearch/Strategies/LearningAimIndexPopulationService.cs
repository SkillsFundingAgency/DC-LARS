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
        private const int PageSize = 3000;
        private readonly ILarsContextFactory _contextFactory;
        private readonly IAcademicYearService _academicYearService;

        public LearningAimIndexPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory,
            IAcademicYearService academicYearService)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
            _academicYearService = academicYearService;
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

                while (next)
                {
                    var learningAims = context.LarsLearningDeliveries
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
                            LevelDescription = ld.NotionalNvqlevelNavigation.NotionalNvqlevelDesc,
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
                                    EndDate = lv.EndDate
                                }).ToList(),
                            FundingModels = ld.LarsFundings
                               .Select(lf => new FundingModel
                               {
                                   EffectiveFrom = lf.EffectiveFrom,
                                   EffectiveTo = lf.EffectiveTo
                               }).ToList()
                        })
                        .OrderBy(ld => ld.LearnAimRef)
                        .ThenBy(ld => ld.EffectiveFrom)
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .ToArray();

                    foreach (var learningDelivery in learningAims)
                    {
                        learningDelivery.AcademicYears =
                            academicYears.Select(ay => new AcademicYearModel
                            {
                                AcademicYear = ay.AcademicYear,
                                Validities = learningDelivery.ValidityModels.Where(lv => lv.StartDate <= ay.EndDate || (lv.EndDate ?? DateTime.MaxValue) >= ay.StartDate).ToList(),
                                Fundings = learningDelivery.FundingModels.Where(lf => lf.EffectiveFrom <= ay.EndDate || (lf.EffectiveTo ?? DateTime.MaxValue) >= ay.StartDate).ToList()
                            }).ToList();

                        learningDelivery.ValidityModels = null;
                        learningDelivery.FundingModels = null;
                    }

                    var indexActions = learningAims.Select(IndexAction.Upload);

                    var batch = IndexBatch.New(indexActions);

                    if (batch.Actions.Any())
                    {
                        var startTime = DateTime.Now;
                        indexClient.Documents.Index(batch);
                        var endTime = DateTime.Now;

                        var duration = endTime - startTime;
                        page++;
                        Console.WriteLine($"Processed {page * PageSize} learning aim documents, (batch time: {duration.Minutes} mins, {duration.Seconds} secs, {duration.Milliseconds} ms) ...");
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