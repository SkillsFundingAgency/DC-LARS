using ESFA.DC.LARS.Web.Interfaces;

namespace ESFA.DC.LARS.Web.Services
{
    public class ClientValidationService : IClientValidationService
    {
        private const int MaxSearchTermLength = 200;
        private const int MaxFilterLength = 150;

        private readonly string _searchTermError = $"The search term must be less than {MaxSearchTermLength} characters long.";
        private readonly string _filterError = $"The filter search term must be less than {MaxFilterLength} characters long.";

        public string SearchTermLengthValid(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return string.Empty;
            }

            return searchTerm.Length > MaxSearchTermLength
                ? _searchTermError
                : string.Empty;
        }

        public string FilterLengthValid(string filterTerm)
        {
            if (string.IsNullOrEmpty(filterTerm))
            {
                return string.Empty;
            }

            return filterTerm.Length > MaxFilterLength
                ? _filterError
                : string.Empty;
        }
    }
}