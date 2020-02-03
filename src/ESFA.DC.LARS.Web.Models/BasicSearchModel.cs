using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class BasicSearchModel
    {
        public string SearchTerm { get; set; }

        public string AwardingBody { get; set; }

        public string Level { get; set; }

        public string TeachingYear { get; set; }
    }
}