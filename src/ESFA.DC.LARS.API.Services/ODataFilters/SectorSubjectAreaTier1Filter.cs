using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class SectorSubjectAreaTier1Filter : BaseFilter, IFrameworkODataFilter
    {
        protected override string FilterODataString => "SectorSubjectAreaTier1 eq '{0}'";

        public string ApplyFilter(FrameworkSearchModel searchModel)
        {
            return ApplyFilter(searchModel.SectorSubjectAreaTier1s);
        }
    }
}
