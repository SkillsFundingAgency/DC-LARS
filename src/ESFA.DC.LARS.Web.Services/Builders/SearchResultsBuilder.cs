using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;

namespace ESFA.DC.LARS.Web.Services.Builders
{
    public class SearchResultsBuilder : ISearchResultsBuilder
    {
        private readonly IMapper<LearningAimModel, Models.LearningAimModel> _learningAimMapper;
        private readonly IMapper<FrameworkModel, Models.FrameworkModel> _frameworkMapper;

        public SearchResultsBuilder(
            IMapper<LearningAimModel, Models.LearningAimModel> learningAimMapper,
            IMapper<FrameworkModel, Models.FrameworkModel> frameworkMapper)
        {
            _learningAimMapper = learningAimMapper;
            _frameworkMapper = frameworkMapper;
        }

        public LearningAimsSearchResultsViewModel BuildLearningAimsSearchResultsViewModel(IEnumerable<LearningAimModel> learningAimModels)
        {
            return new LearningAimsSearchResultsViewModel
            {
                LearningAimModels = learningAimModels.Select(ld => _learningAimMapper.Map(ld))
            };
        }

        public FrameworkSearchResultsViewModel BuildLearningAimsSearchResultsViewModel(IEnumerable<FrameworkModel> frameworkModels)
        {
            return new FrameworkSearchResultsViewModel
            {
                FrameworkModels = frameworkModels.Select(ld => _frameworkMapper.Map(ld)).ToList()
            };
        }
    }
}