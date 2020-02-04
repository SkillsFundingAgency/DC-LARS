using System;
using System.Linq;
using System.Threading;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class LearningAimIndexPopulationService : AbstractPopulationService<LearningAimModel>
    {
        public LearningAimIndexPopulationService(ISearchServiceClient searchServiceClient, IPopulationConfiguration populationConfiguration)
            : base(searchServiceClient, populationConfiguration)
        {
        }

        protected override string IndexName => _populationConfiguration.LearningAimsIndexName;

        public override void PopulateIndex()
        {
            var indexClient = GetIndexClient();

            var config = new DbContextOptionsBuilder<LarsContext>();

            config.UseSqlServer(ConnectionString);

            var next = true;

            using (var context = new LarsContext(config.Options))
            {
                var page = 0;

                while (next)
                {
                    var learningAims = context.LarsLearningDeliveries
                        .Select(ld => new LearningAimModel
                        {
                            LearnAimRef = ld.LearnAimRef,
                            AwardingBody = ld.AwardOrgCode,
                            EffectiveFrom = ld.EffectiveFrom,
                            EffectiveTo = ld.EffectiveTo,
                            Level = ld.NotionalNvqlevelv2Navigation.NotionalNvqlevelV2,
                            LevelDescription = ld.NotionalNvqlevelNavigation.NotionalNvqlevelDesc,
                            Type = ld.LearnAimRefTypeNavigation.LearnAimRefTypeDesc,
                            LearningAimTitle = ld.LearnAimRefTitle,
                            GuidedLearningHours = ld.GuidedLearningHours ?? 0,
                            Categories = ld.LarsLearningDeliveryCategories.Select(cat => new CategoryModel
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
                            }).ToList()
                        })
                        .OrderBy(ld => ld.LearnAimRef)
                        .ThenBy(ld => ld.EffectiveFrom)
                        .Skip(page * 10000)
                        .Take(10000)
                        .ToArray();

                    var indexActions = learningAims.Select(IndexAction.Upload);

                    var batch = IndexBatch.New(indexActions);

                    if (batch.Actions.Any())
                    {
                        indexClient.Documents.Index(batch);
                        page++;
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