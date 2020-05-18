using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public abstract class AbstractSearchModel
    {
        public string SearchTerm { get; set; }

        public List<string> TeachingYears { get; set; }

        public abstract LearningType? SearchType { get; set; }
    }
}
