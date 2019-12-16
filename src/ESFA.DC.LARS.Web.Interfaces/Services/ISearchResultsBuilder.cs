using System.Collections.Generic;
using ESFA.DC.LARS.Web.Models.ViewModels;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface ISearchResultsBuilder
    {
        SearchResultsViewModel BuildSearchResultsViewModel(IEnumerable<API.Models.LearningAimModel> learningAimModels);
    }
}