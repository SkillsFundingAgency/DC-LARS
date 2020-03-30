using ESFA.DC.LARS.API.Models;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IODataQueryService
    {
        void SetLearningAimFilters(LearningAimsSearchModel searchModel, SearchParameters parameters);

        void SetFrameworkFilters(FrameworkSearchModel searchModel, SearchParameters parameters);
    }
}