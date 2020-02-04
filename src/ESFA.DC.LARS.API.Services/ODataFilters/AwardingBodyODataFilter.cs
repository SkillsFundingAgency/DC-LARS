using System.Linq;
using System.Text;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class AwardingBodyODataFilter : IODataFilter
    {
        public string ApplyFilter(SearchModel searchModel)
        {
            if (!(searchModel.AwardingBodies?.Any() ?? false))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("(");
            foreach (var body in searchModel.AwardingBodies)
            {
                stringBuilder.Append(stringBuilder.Length == 1
                    ? $"AwardingBodyCode eq '{body}' or AwardingBodyName eq '{body}'"
                    : $" or AwardingBodyCode eq '{body}' or AwardingBodyName eq '{body}'");
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}