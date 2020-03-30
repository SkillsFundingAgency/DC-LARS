using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureIssuingAuthorityLookupMapper : IMapper<IssuingAuthorityLookupModel, Models.IssuingAuthorityLookupModel>
    {
        public Models.IssuingAuthorityLookupModel Map(IssuingAuthorityLookupModel input)
        {
            return new Models.IssuingAuthorityLookupModel
            {
                IssuingAuthority = input.IssuingAuthority,
                IssuingAuthorityDesc = input.IssuingAuthorityDesc
            };
        }
    }
}