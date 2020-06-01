using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.LARS.Web.Models
{
    public class StandardFundingModel
    {
        public string FundingCategoryDescription { get; set; }

        public int? BandNumber { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string CoreGovContributionCap { get; set; }

        public string Incentive1618 { get; set; }

        public string SmallBusinessIncentive { get; set; }

        public string AchievementIncentive { get; set; }
    }
}
