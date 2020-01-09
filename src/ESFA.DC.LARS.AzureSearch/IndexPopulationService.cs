using System;
using System.Linq;
using System.Threading;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.Azure.Search;
using Microsoft.EntityFrameworkCore;
using Index = Microsoft.Azure.Search.Models.Index;
using IndexAction = Microsoft.Azure.Search.Models.IndexAction;
using IndexBatch = Microsoft.Azure.Search.Models.IndexBatch;

namespace ESFA.DC.LARS.AzureSearch
{
    public class IndexPopulationService : IIndexPopulationService
    {
        public void UploadDocuments(ISearchIndexClient indexClient, ConnectionStrings connectionStrings)
        {
            var config = new DbContextOptionsBuilder<LarsContext>();

            config.UseSqlServer(connectionStrings.LarsConnectionString);

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
                            Level = ld.NotionalNvqlevelv2,
                            LearningAimTitle = ld.LearnAimRefTitle,
                            FundingModels = ld.LarsFundings.Select(s => new FundingModel()
                            {
                                FundingCategoryDescription = s.FundingCategory,
                                EffectiveFrom = s.EffectiveFrom,
                                EffectiveTo = s.EffectiveTo,
                                RateUnWeighted = s.RateUnWeighted.HasValue ? s.RateUnWeighted.ToString() : null,
                                RateWeighted = s.RateWeighted.HasValue ? s.RateWeighted.ToString() : null,
                                WeightingFactor = s.WeightingFactor,
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

        public void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists(indexName))
            {
                serviceClient.Indexes.Delete(indexName);
            }
        }

        public void CreateIndex(string indexName, ISearchServiceClient serviceClient)
        {
            var definition = new Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<LearningAimModel>()
            };

            serviceClient.Indexes.Create(definition);
        }
    }
}