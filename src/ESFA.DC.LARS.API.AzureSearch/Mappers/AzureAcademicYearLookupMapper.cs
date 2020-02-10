using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureAcademicYearLookupMapper : IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel>
    {
        public Models.AcademicYearLookupModel Map(AcademicYearLookupModel input)
        {
            return new Models.AcademicYearLookupModel
            {
                IsCurrentAcademicYear = input.IsCurrentAcademicYear,
                AcademicYear = input.AcademicYear,
                AcademicYearDesc = input.AcademicYearDesc
            };
        }
    }
}