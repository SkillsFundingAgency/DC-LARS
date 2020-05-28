using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class StandardLevelODataFilter : BaseFilter, IStandardODataFilter
    {
        protected override string FilterODataString => "NotionalEndLevel eq '{0}'";

        public string ApplyFilter(StandardSearchModel searchModel)
        {
            return ApplyFilter(searchModel.Levels);
        }
    }
}