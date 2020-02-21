using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimsMapper : IMapper<LearningAimModel, Models.LearningAimModel>
    {
        private readonly IMapper<CategoryModel, Models.CategoryModel> _categoryMapper;
        private readonly IMapper<AcademicYearModel, Models.AcademicYearModel> _academicYearMapper;

        public AzureLearningAimsMapper(
            IMapper<CategoryModel, Models.CategoryModel> categoryMapper,
            IMapper<AcademicYearModel, Models.AcademicYearModel> academicYearMapper)
        {
            _categoryMapper = categoryMapper;
            _academicYearMapper = academicYearMapper;
        }

        public Models.LearningAimModel Map(LearningAimModel input)
        {
            return new Models.LearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                Type = input.Type,
                AwardingBodyCode = input.AwardingBodyCode,
                AwardingBodyName = input.AwardingBodyName,
                Level = input.LevelDescription,
                GuidedLearningHours = input.GuidedLearningHours,
                Categories = input.Categories?.Select(cat => _categoryMapper.Map(cat)).ToList(),
                AcademicYears = input.AcademicYears?.Select(ay => _academicYearMapper.Map(ay)).ToList()
            };
        }
    }
}