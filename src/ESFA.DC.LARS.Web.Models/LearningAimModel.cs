using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class LearningAimModel
    {
        public string LearnAimRef { get; set; }

        public string LearningAimTitle { get; set; }

        public string Level { get; set; }

        public string Type { get; set; }

        public int GuidedLearningHours { get; set; }

        public string AwardingBodyCode { get; set; }

        public string AwardingBodyName { get; set; }

        public string Level2Category { get; set; }

        public string Level3Category { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<AcademicYearModel> AcademicYears { get; set; }

        public List<LearningAimFrameworkModel> Frameworks { get; set; }
    }
}