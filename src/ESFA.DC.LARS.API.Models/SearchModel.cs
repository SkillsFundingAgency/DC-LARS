using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.API.Models
{
    [ExcludeFromCodeCoverage]
    public class SearchModel
    {
        public string SearchTerm { get; set; }

        public string AwardingBody { get; set; }

        public string Level { get; set; }

        public string TeachingYear { get; set; }

        public SearchFilterModel SearchFilters { get; set; }
    }
}