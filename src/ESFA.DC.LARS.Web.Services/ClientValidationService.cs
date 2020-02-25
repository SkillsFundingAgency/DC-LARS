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
            return LengthValid(searchTerm, MaxSearchTermLength) ? string.Empty : _searchTermError;
        }

        public string FilterLengthValid(string filterTerm)
        {
            return LengthValid(filterTerm, MaxFilterLength) ? string.Empty : _filterError;
        }

        private bool LengthValid(string term, int maxLength)
        {
            if (string.IsNullOrEmpty(term))
            {
                return true;
            }

            return term.Length <= maxLength;
        }
    }
}