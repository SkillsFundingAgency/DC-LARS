using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureStandardSectorLookupMapper : IMapper<StandardSectorLookupModel, Models.StandardSectorLookupModel>
    {
        Models.StandardSectorLookupModel IMapper<StandardSectorLookupModel, Models.StandardSectorLookupModel>.Map(StandardSectorLookupModel input)
        {
            return new Models.StandardSectorLookupModel
            {
                StandardSectorCode = input.StandardSectorCode,
                StandardSectorCodeDesc = input.StandardSectorCodeDesc
            };
        }
    }
}
