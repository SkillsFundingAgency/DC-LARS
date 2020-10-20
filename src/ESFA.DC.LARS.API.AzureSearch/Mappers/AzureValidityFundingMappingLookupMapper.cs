using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureValidityFundingMappingLookupMapper : IMapper<ValidityFundingMappingLookupModel, Models.ValidityFundingMappingLookupModel>
    {
        public Models.ValidityFundingMappingLookupModel Map(ValidityFundingMappingLookupModel input)
        {
            return new Models.ValidityFundingMappingLookupModel
            {
                ValidityCategory = input.ValidityCategory,
                FundingCategory = input.FundingCategory,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo
            };
        }
    }
}