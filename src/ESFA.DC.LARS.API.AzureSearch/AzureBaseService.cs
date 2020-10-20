using ESFA.DC.LARS.API.Interfaces.Services;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public abstract class AzureBaseService
    {
        private ISearchTermFormattingService _searchTermFormattingService;

        public AzureBaseService(ISearchTermFormattingService searchTermFormattingService)
        {
            _searchTermFormattingService = searchTermFormattingService;
        }

        protected SearchParameters GetDefaultParameters()
        {
            return new SearchParameters
            {
                QueryType = QueryType.Full,
                SearchMode = SearchMode.Any,
                IncludeTotalResultCount = true,
                Top = 10000
            };
        }

        protected string FormatSearchTerm(string searchTerm)
        {
            return _searchTermFormattingService.FormatSearchTerm(searchTerm);
        }
    }
}