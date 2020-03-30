using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class AcademicYearODataFilter : BaseFilter, IODataFilter
    {
        protected override string FilterODataString => "AcademicYears/any(ay:ay/AcademicYear eq '{0}')";

        public string ApplyFilter(LearningAimsSearchModel searchModel)
        {
            return ApplyFilter(searchModel.TeachingYears);
        }
    }
}