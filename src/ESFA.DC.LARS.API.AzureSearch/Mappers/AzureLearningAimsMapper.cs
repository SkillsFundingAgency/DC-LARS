using System.Linq;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimsMapper : IMapper<LearningAimModel, Models.LearningAimModel>
    {
        private readonly IMapper<CategoryModel, Models.CategoryModel> _categoryMapper;
        private readonly IMapper<FundingModel, Models.FundingModel> _fundingModelMapper;

        public AzureLearningAimsMapper(
            IMapper<CategoryModel, Models.CategoryModel> categoryMapper,
            IMapper<FundingModel, Models.FundingModel> fundingModelMapper)
        {
            _categoryMapper = categoryMapper;
            _fundingModelMapper = fundingModelMapper;
        }

        public Models.LearningAimModel Map(LearningAimModel input)
        {
            return new Models.LearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                Type = input.Type,
                AwardingBody = input.AwardingBody,
                Level = input.Level,
                GuidedLearningHours = input.GuidedLearningHours,
                Level2Category = input.Level2Category,
                Level3Category = input.Level3Category,
                Categories = input.Categories.Select(_categoryMapper.Map).ToList(),
                FundingModels = input.FundingModels.Select(_fundingModelMapper.Map).ToList()
            };
        }
    }
}