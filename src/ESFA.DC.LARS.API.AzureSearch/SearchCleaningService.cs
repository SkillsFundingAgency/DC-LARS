using System.Linq;
using ESFA.DC.LARS.API.Interfaces;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class SearchCleaningService : ISearchCleaningService
    {
        private const string SpecialChars = @"+-&|!(){}[]^""~*?:;/`<>#%@=";
        private const string EscapeCharacter = "\\";

        public string EscapeSpecialCharacters(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return term;
            }

            term = term.Replace("\\", "\\\\");
            foreach (var c in SpecialChars.Where(c => term.Contains(c)))
            {
                term = term.Replace($"{c}", $"{EscapeCharacter}{c}");
            }

            return term;
        }
    }
}