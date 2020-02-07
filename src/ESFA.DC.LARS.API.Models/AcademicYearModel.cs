using System.Collections.Generic;

namespace ESFA.DC.LARS.API.Models
{
    public class AcademicYearModel
    {
        public string AcademicYear { get; set; }

        public List<ValidityModel> Validities { get; set; }

        public List<FundingModel> Fundings { get; set; }
    }
}