using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Factories
{
    public class SearchModelFactory : ISearchModelFactory
    {
        public LearningAimsSearchModel GetLearningAimsSearchModel(BasicSearchModel basicSearchModel)
        {
            var searchModel = new LearningAimsSearchModel();

            if (!string.IsNullOrEmpty(basicSearchModel.SearchTerm))
            {
                searchModel.SearchTerm = basicSearchModel.SearchTerm;
            }

            if (!string.IsNullOrEmpty(basicSearchModel.Level))
            {
                searchModel.Levels.Add(basicSearchModel.Level);
            }

            if (!string.IsNullOrEmpty(basicSearchModel.AwardingBody))
            {
                searchModel.AwardingBodies.Add(basicSearchModel.AwardingBody);
            }

            if (!string.IsNullOrEmpty(basicSearchModel.TeachingYear))
            {
                searchModel.TeachingYears.Add(basicSearchModel.TeachingYear);
            }

            return searchModel;
        }

        public FrameworkSearchModel GetFrameworkSearchModel(BasicSearchModel basicSearchModel)
        {
            var searchModel = new FrameworkSearchModel();

            if (!string.IsNullOrEmpty(basicSearchModel.SearchTerm))
            {
                searchModel.SearchTerm = basicSearchModel.SearchTerm;
            }

            return searchModel;
        }

        public StandardSearchModel GetStandardSearchModel(BasicSearchModel basicSearchModel)
        {
            var searchModel = new StandardSearchModel();

            if (!string.IsNullOrEmpty(basicSearchModel.SearchTerm))
            {
                searchModel.SearchTerm = basicSearchModel.SearchTerm;
            }

            return searchModel;
        }
    }
}