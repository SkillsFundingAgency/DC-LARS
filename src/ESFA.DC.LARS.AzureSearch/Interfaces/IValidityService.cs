using System.Collections.Generic;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IValidityService
    {
        Dictionary<string, List<ValidityModel>> GetValidities(LarsContext context);

        List<ValidityModel> GetValidityByLearnRef(Dictionary<string, List<ValidityModel>> validities, string learnRef);
    }
}