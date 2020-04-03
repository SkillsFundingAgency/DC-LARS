using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class BreadcrumbsModel
    {
        public string Id { get; set; }

        public Dictionary<string, string> Breadcrumbs { get; set; }
    }
}
