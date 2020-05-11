using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class FrameworkSearchModel : BaseSearchModel
    {
        public List<string> FrameworkTypes { get; set; } = new List<string>();

        public List<string> IssuingAuthorities { get; set; } = new List<string>();
    }
}