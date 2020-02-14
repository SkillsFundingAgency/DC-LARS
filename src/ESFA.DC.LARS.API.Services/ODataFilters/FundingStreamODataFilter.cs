using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class FundingStreamODataFilter : BaseFilter, IODataFilter
    {
        protected override string FilterODataString => "AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{0}'))";

        public string ApplyFilter(SearchModel searchModel)
        {
            return ApplyFilter(searchModel.FundingStreams);
        }
    }
}