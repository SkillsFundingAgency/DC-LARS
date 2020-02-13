using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureAcademicYearMapper : IMapper<AcademicYearModel, Models.AcademicYearModel>
    {
        private readonly IMapper<FundingModel, Models.FundingModel> _fundingMapper;
        private readonly IMapper<ValidityModel, Models.ValidityModel> _validityMapper;

        public AzureAcademicYearMapper(
            IMapper<FundingModel, Models.FundingModel> fundingMapper,
            IMapper<ValidityModel, Models.ValidityModel> validityMapper)
        {
            _fundingMapper = fundingMapper;
            _validityMapper = validityMapper;
        }

        public Models.AcademicYearModel Map(AcademicYearModel input)
        {
            return new Models.AcademicYearModel
            {
                AcademicYear = input.AcademicYear,
                Level2Category = input.Level2Category,
                Level3Category = input.Level3Category,
                Fundings = input.Fundings.Select(_fundingMapper.Map).ToList(),
                Validities = input.Validities.Select(_validityMapper.Map).ToList()
            };
        }
    }
}