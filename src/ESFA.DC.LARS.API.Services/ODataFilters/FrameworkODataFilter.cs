using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.Services;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class FrameworkODataFilter : IFrameworkODataFilter
    {
        private readonly ISearchCleaningService _searchCleaningService;
        private readonly string _frameworkQueryString = "FrameworkCode eq {0} and ProgramType eq {1} and PathwayCode eq {2}";

        public FrameworkODataFilter(ISearchCleaningService searchCleaningService)
        {
            _searchCleaningService = searchCleaningService;
        }

        public string GetFilter(int frameworkCode, int programType, int pathwayCode)
        {
            var filter = _searchCleaningService.EscapeFilterSpecialCharacters(
                string.Format(_frameworkQueryString, frameworkCode, programType, pathwayCode));

            return filter;
        }
    }
}