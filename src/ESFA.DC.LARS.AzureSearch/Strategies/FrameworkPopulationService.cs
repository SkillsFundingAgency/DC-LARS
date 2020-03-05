using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class FrameworkPopulationService : AbstractPopulationService<FrameworkModel>
    {
        private readonly ILarsContextFactory _contextFactory;

        public FrameworkPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            ILarsContextFactory contextFactory)
            : base(searchServiceClient, populationConfiguration)
        {
            _contextFactory = contextFactory;
        }

        protected override string IndexName => _populationConfiguration.FrameworkIndexName;

        public override void PopulateIndex()
        {
            var indexClient = GetIndexClient();

            IEnumerable<FrameworkModel> frameworks;

            IDictionary<string, string> issuingAuthorities;

            using (var context = _contextFactory.GetLarsContext())
            {
                issuingAuthorities =
                    context.LarsIssuingAuthorityLookups
                        .ToDictionary(
                            ia => ia.IssuingAuthority.ToString(), // field is different type between tables...
                            ia => ia.IssuingAuthorityDesc);

                frameworks = context.LarsFrameworks
                    .Select(fr => new FrameworkModel
                    {
                        Id = string.Concat(fr.FworkCode, fr.ProgType, fr.PwayCode), // azure search index must have 1 key field
                        FrameworkCode = fr.FworkCode,
                        ProgramType = fr.ProgType,
                        PathwayCode = fr.PwayCode,
                        PathwayName = fr.PathwayName,
                        FrameworkTitle = fr.IssuingAuthorityTitle,
                        EffectiveFrom = fr.EffectiveFrom,
                        EffectiveTo = fr.EffectiveTo,
                        SectorSubjectAreaTier2 = fr.SectorSubjectAreaTier2.ToString(), // decimal not supported by azure
                        SectorSubjectAreaTier2Desc = fr.SectorSubjectAreaTier2Navigation.SectorSubjectAreaTier2Desc,
                        IssuingAuthority = fr.IssuingAuthority,
                        IssuingAuthorityDesc = issuingAuthorities[fr.IssuingAuthority]
                    }).ToList();
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