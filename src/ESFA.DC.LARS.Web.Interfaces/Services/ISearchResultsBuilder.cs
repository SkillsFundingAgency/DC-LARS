using System.Collections.Generic;
using ESFA.DC.LARS.Web.Models.ViewModels;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface ISearchResultsBuilder
    {
        LearningAimsSearchResultsViewModel BuildLearningAimsSearchResultsViewModel(IEnumerable<API.Models.LearningAimModel> learningAimModels);

        FrameworkSearchResultsViewModel BuildLearningAimsSearchResultsViewModel(IEnumerable<API.Models.FrameworkModel> frameworkModels);
    }
}