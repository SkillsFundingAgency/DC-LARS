using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.LARS.Web.Models
{
    public class StandardApprenticeshipFundingModel
    {
        public string FundingCategoryDescription { get; set; }

        public int? BandNumber { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string ProviderAdditionalPayment1618 { get; set; }

        public string EmployerAdditionalPayment1618 { get; set; }

        public string FrameworkUplift1618 { get; set; }

        public string CareLeaverAdditionalPayment { get; set; }

        public string Duration { get; set; }

        public string MaxEmployerLevyCap { get; set; }

        public string FundableWithoutEmployer { get; set; }
    }
}
