using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public abstract class AbstractSearchResultsViewModel
    {
        public abstract string SearchTerm { get; }

        public abstract LearningType? SearchType { get; }

        public List<string> ValidationErrors { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public string CurrentAcademicYear => LookUpModel.AcademicYearLookups.Single(m => m.IsCurrentAcademicYear).AcademicYear;

        public bool IsLoading { get; set; }
    }
}
