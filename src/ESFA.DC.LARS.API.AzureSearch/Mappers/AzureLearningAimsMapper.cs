using System;
using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimsMapper : IMapper<LearningAimModel, Models.LearningAimModel>
    {
        private readonly IMapper<CategoryModel, Models.CategoryModel> _categoryMapper;
        private readonly IMapper<AcademicYearModel, Models.AcademicYearModel> _academicYearMapper;
        private readonly IMapper<LearningAimFrameworkModel, Models.LearningAimFrameworkModel> _frameworkMapper;

        public AzureLearningAimsMapper(
            IMapper<CategoryModel, Models.CategoryModel> categoryMapper,
            IMapper<AcademicYearModel, Models.AcademicYearModel> academicYearMapper,
            IMapper<LearningAimFrameworkModel, Models.LearningAimFrameworkModel> frameworkMapper)
        {
            _categoryMapper = categoryMapper;
            _academicYearMapper = academicYearMapper;
            _frameworkMapper = frameworkMapper;
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
                AcademicYears = input.AcademicYears?.Select(ay => _academicYearMapper.Map(ay)).ToList(),
                Frameworks = input.Frameworks?.Select(fr => _frameworkMapper.Map(fr))
                                                .OrderBy(f => f.FrameworkTitle)
                                                .ThenBy(f => f.ProgramType)
                                                .ThenByDescending(f => f.PathwayCode)
                                                .ToList()
            };
        }
    }
}