using System.Collections.Generic;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    public class AcademicYearModel
    {
        [IsFilterable]
        public string AcademicYear { get; set; }

        public string Level2Category { get; set; }

        public string Level3Category { get; set; }

        public List<ValidityModel> Validities { get; set; }

        public List<FundingModel> Fundings { get; set; }
    }
}