using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureStandardApprenticeshipFundingModelMapper : IMapper<StandardApprenticeshipFundingModel, Models.StandardApprenticeshipFundingModel>
    {
        public Models.StandardApprenticeshipFundingModel Map(StandardApprenticeshipFundingModel input)
        {
            return new Models.StandardApprenticeshipFundingModel()
            {
                FundingCategoryDescription = input.FundingCategoryDescription,
                BandNumber = input.BandNumber,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                ProviderAdditionalPayment1618 = input.ProviderAdditionalPayment1618,
                EmployerAdditionalPayment1618 = input.EmployerAdditionalPayment1618,
                FrameworkUplift1618 = input.FrameworkUplift1618,
                CareLeaverAdditionalPayment = input.CareLeaverAdditionalPayment,
                Duration = input.Duration,
                MaxEmployerLevyCap = input.MaxEmployerLevyCap,
                FundableWithoutEmployer = input.FundableWithoutEmployer,
            };
        }
    }
}