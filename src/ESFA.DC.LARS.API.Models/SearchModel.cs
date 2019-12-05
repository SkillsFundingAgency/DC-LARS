namespace ESFA.DC.LARS.API.Models
{
    public class SearchModel
    {
        public string SearchTerm { get; set; }

        public string AwardingBody { get; set; }

        public string Level { get; set; }

        public string TeachingYear { get; set; }

        public SearchFilterModel SearchFilters { get; set; }
    }
}