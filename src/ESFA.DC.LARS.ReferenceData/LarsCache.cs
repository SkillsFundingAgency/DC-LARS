using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.ReferenceData
{
    public class LarsCache : ILarsCache
    {
        private readonly ILarsJsonService _larsJsonService;

        public LarsCache(ILarsJsonService larsJsonService)
        {
            _larsJsonService = larsJsonService;
        }

        private static IEnumerable<LearningAimModel> LearningAims { get; set; }

        public async Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchModel, bool isDirty)
        {
            if (LearningAims == null || isDirty)
            {
                LearningAims = await _larsJsonService.GetLarsLearningDeliveriesFromJsonFile();
            }

            var filteredAims = FilterDeliveryMatchesSearch(LearningAims, searchModel);

            return filteredAims;
        }

        private IEnumerable<LearningAimModel> FilterDeliveryMatchesSearch(IEnumerable<LearningAimModel> deliveries, SearchModel searchModel)
        {
            if (searchModel == null)
            {
                return deliveries;
            }

            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                deliveries = deliveries.Where(ld =>
                    ld.LearningAimTitle.Contains(searchModel.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    ld.LearnAimRef.Equals(searchModel.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchModel.AwardingBody))
            {
                deliveries = deliveries.Where(ld => ld.AwardingBody.Contains(searchModel.AwardingBody));
            }

            if (searchModel.SearchFilters != null)
            {
                if (searchModel.SearchFilters?.AwardingBodies.Any() ?? false)
                {
                    deliveries = deliveries.Where(ld => searchModel.SearchFilters.AwardingBodies.Any(ab => ab.Contains(ld.AwardingBody, StringComparison.OrdinalIgnoreCase)));
                }

                if (searchModel.SearchFilters?.Levels.Any() ?? false)
                {
                    deliveries = deliveries.Where(ld => searchModel.SearchFilters.Levels.Any(ab => ab.Contains(ld.Level, StringComparison.OrdinalIgnoreCase)));
                }
            }

            return deliveries;
        }
    }
}