using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESFA.DC.LARS.API.Interfaces;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class SearchCleaningService : ISearchCleaningService
    {
        private const string SearchEscapeCharacter = @"\";

        private readonly IEnumerable<char> _luceneSpecialCharacters = new HashSet<char> { '+', '-', '&', '|', '!', '(', ')', '{', '}', '[', ']', '^', '"', '~', '?', ':', ';', '/', '`', '<', '>', '#', '%', '@', '=', '\\' };

        public string EscapeSearchSpecialCharacters(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return term;
            }

            var stringBuilder = new StringBuilder();

            foreach (var character in term)
            {
                if (_luceneSpecialCharacters.Contains(character))
                {
                    stringBuilder.Append(SearchEscapeCharacter);
                }

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }

        public string EscapeFilterSpecialCharacters(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return term;
            }

            return term.Replace("'", "''");
        }
    }
}