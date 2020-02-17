using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureAwardingBodyLookupMapper : IMapper<AwardingBodyLookupModel, Models.AwardingBodyLookupModel>
    {
        public Models.AwardingBodyLookupModel Map(AwardingBodyLookupModel input)
        {
            return new Models.AwardingBodyLookupModel
            {
                AwardingBodyCode = input.AwardingBodyCode,
                AwardingBodyName = input.AwardingBodyName
            };
        }
    }
}