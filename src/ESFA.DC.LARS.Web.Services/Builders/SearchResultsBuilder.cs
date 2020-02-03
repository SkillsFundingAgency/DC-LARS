using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;

namespace ESFA.DC.LARS.Web.Services.Builders
{
    public class SearchResultsBuilder : ISearchResultsBuilder
    {
        private readonly IMapper<LearningAimModel, Models.LearningAimModel> _mapper;

        public SearchResultsBuilder(
            IMapper<LearningAimModel, Models.LearningAimModel> mapper)
        {
            _mapper = mapper;
        }

        public SearchResultsViewModel BuildSearchResultsViewModel(IEnumerable<LearningAimModel> learningAimModels)
        {
            return new SearchResultsViewModel
            {
                LearningAimModels = learningAimModels.Select(ld => _mapper.Map(ld))
            };
        }
    }
}