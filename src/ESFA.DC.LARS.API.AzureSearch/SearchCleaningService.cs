using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESFA.DC.LARS.API.Interfaces;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class SearchCleaningService : ISearchCleaningService
    {
        private const string SpecialChars = @"+-&|!(){}[]^""~*?:;/`<>#%@=";
        private const string EscapeCharacter = @"\";

        private readonly IEnumerable<char> _specialCharacters = new HashSet<char>('$');

        public string EscapeSpecialCharacters(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return term;
            }

            term = term.Replace(@"\", @"\\");

            var stringBuilder = new StringBuilder();

            foreach (var character in term)
            {
                if (_specialCharacters.Contains(character))
                {
                    stringBuilder.Append(EscapeCharacter);
                }

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }
    }
}