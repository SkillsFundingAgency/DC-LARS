﻿using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Interfaces.Services;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureSearchTermFormattingService : ISearchTermFormattingService
    {
        public string FormatSearchTerm(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return string.Empty;
            }

            if (HasTermWithPrefixedWildCard(searchTerm))
            {
                return ConvertTermToRegularExpression(searchTerm);
            }

            return searchTerm.Trim();
        }

        private bool HasTermWithPrefixedWildCard(string searchTerm)
        {
            return searchTerm.StartsWith("*") || searchTerm.Contains(" *");
        }

        private string ConvertTermToRegularExpression(string searchTerm)
        {
            var searchTermWords = GetSearchTermAsListOfWords(searchTerm);

            searchTermWords = searchTermWords.Select(w => ConvertPrefixWildCardsToRegEx(w)).ToList();

            return string.Join(" ", searchTermWords);
        }

        private List<string> GetSearchTermAsListOfWords(string searchTerm)
        {
            return searchTerm.Split(' ')
                             .Where(w => !string.IsNullOrWhiteSpace(w))
                             .Select(w => w.Trim()).ToList();
        }

        private string ConvertPrefixWildCardsToRegEx(string searchWord)
        {
            if (searchWord.StartsWith("*"))
            {
                return $"/{searchWord.Replace("*", ".*")}/";
            }

            return searchWord;
        }
    }
}
