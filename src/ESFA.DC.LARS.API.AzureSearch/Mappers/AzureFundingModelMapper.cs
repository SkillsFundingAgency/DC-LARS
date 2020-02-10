using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureFundingModelMapper : IMapper<FundingModel, Models.FundingModel>
    {
        public Models.FundingModel Map(FundingModel input)
        {
            return new Models.FundingModel
            {
                LearnAimRef = input.LearnAimRef,
                FundingCategoryDescription = input.FundingCategoryDescription,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                WeightingFactor = input.WeightingFactor,
                RateWeighted = input.RateWeighted,
                RateUnWeighted = input.RateUnWeighted
            };
        }
    }
}