using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureStandardFundingModelMapper : IMapper<StandardFundingModel, Models.StandardFundingModel>
    {
        public Models.StandardFundingModel Map(StandardFundingModel input)
        {
            return new Models.StandardFundingModel()
            {
                FundingCategoryDescription = input.FundingCategoryDescription,
                BandNumber = input.BandNumber,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                CoreGovContributionCap = input.CoreGovContributionCap,
                Incentive1618 = input.Incentive1618,
                SmallBusinessIncentive = input.SmallBusinessIncentive,
                AchievementIncentive = input.AchievementIncentive
            };
        }
    }
}