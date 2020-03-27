using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface ISearchModelFactory
    {
        LearningAimsSearchModel GetLearningAimsSearchModel(BasicSearchModel basicSearchModel);

        FrameworkSearchModel GetFrameworkSearchModel(BasicSearchModel basicSearchModel);
    }
}