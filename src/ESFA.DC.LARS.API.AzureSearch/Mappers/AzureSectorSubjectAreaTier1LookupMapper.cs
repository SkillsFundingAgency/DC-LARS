using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureSectorSubjectAreaTier1LookupMapper : IMapper<SectorSubjectAreaTier1LookupModel, Models.SectorSubjectAreaTier1LookupModel>
    {
        public Models.SectorSubjectAreaTier1LookupModel Map(SectorSubjectAreaTier1LookupModel input)
        {
            return new Models.SectorSubjectAreaTier1LookupModel
            {
                SectorSubjectAreaTier1 = input.SectorSubjectAreaTier1,
                SectorSubjectAreaTier1Desc = input.SectorSubjectAreaTier1Desc
            };
        }
    }
}
