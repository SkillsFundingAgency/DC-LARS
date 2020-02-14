using System;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    public class ValidityModel
    {
        [IsFilterable]
        public string ValidityCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? LastNewStartDate { get; set; }

        public string ValidityCategoryDescription { get; set; }
    }
}