using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimDetailsViewModel
    {
        public string FormController { get; set; }

        public string CategoryController { get; set; }

        public string AcademicYear { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public LearningAimModel LearningAimModel { get; set; }

        public IEnumerable<AcademicYearLookupModel> AcademicYearsForLearningAim
        {
            get
            {
                return LookUpModel.AcademicYearLookups.Where(a => LearningAimModel.AcademicYears.Any(y => y.AcademicYear == a.AcademicYear));
            }
        }
    }
}