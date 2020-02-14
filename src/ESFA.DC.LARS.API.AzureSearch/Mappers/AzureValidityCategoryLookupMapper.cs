using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureValidityCategoryLookupMapper : IMapper<ValidityCategoryLookupModel, Models.ValidityCategoryLookupModel>
    {
        public Models.ValidityCategoryLookupModel Map(ValidityCategoryLookupModel input)
        {
            return new Models.ValidityCategoryLookupModel
            {
                ValidityCategory = input.ValidityCategory,
                ValidityCategoryDescription = input.ValidityCategoryDescription
            };
        }
    }
}