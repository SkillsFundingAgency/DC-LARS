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

        //////Used by Application Insight Customer Filter to capture values passed in.
        public override string ToString()
        {
            return $"SearchTerm : {SearchTerm} | TeachingYear : {TeachingYear} | AwardingBody : {AwardingBody} | Level : {Level}";
        }
    }
}