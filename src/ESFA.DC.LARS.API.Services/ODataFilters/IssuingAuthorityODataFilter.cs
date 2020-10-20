using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class IssuingAuthorityODataFilter : BaseFilter, IFrameworkODataFilter
    {
        protected override string FilterODataString => "IssuingAuthority eq '{0}'";

        public string ApplyFilter(FrameworkSearchModel searchModel)
        {
            return ApplyFilter(searchModel.IssuingAuthorities);
        }
    }
}