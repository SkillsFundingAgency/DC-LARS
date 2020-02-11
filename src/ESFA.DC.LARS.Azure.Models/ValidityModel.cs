using System;

namespace ESFA.DC.LARS.Azure.Models
{
    public class ValidityModel
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? LastNewStartDate { get; set; }

        public string ValidityCategory { get; set; }

        public string ValidityCategoryDescription { get; set; }
    }
}