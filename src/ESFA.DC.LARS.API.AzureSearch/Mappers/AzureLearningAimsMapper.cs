using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimsMapper : IMapper<LearningAimModel, Models.LearningAimModel>
    {
        private readonly IMapper<CategoryModel, Models.CategoryModel> _categoryMapper;

        public AzureLearningAimsMapper(
            IMapper<CategoryModel, Models.CategoryModel> categoryMapper)
        {
            _categoryMapper = categoryMapper;
        }

        public Models.LearningAimModel Map(LearningAimModel input)
        {
            return new Models.LearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                Type = input.Type,
                AwardingBody = input.AwardingBody,
                Level = input.LevelDescription,
                GuidedLearningHours = input.GuidedLearningHours,
                Categories = input.Categories?.Select(cat => _categoryMapper.Map(cat)).ToList()
            };
        }
    }
}