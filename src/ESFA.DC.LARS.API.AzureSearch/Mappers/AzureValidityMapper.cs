using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureValidityMapper : IMapper<ValidityModel, Models.ValidityModel>
    {
        public Models.ValidityModel Map(ValidityModel input)
        {
            return new Models.ValidityModel
            {
                StartDate = input.StartDate,
                EndDate = input.EndDate
            };
        }
    }
}