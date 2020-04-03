using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class SearchFilterModel
    {
        public string Name { get; set; }

        public FilterType FilterType { get; set; }

        public string Description { get; set; }

        public List<FilterItemModel> FilterItems { get; set; } = new List<FilterItemModel>();

        public List<string> SelectedFilters { get; set; } = new List<string>();
    }
}
