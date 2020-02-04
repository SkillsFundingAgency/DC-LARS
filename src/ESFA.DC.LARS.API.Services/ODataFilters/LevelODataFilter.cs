using System.Linq;
using System.Text;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class LevelODataFilter : IODataFilter
    {
        public string ApplyFilter(SearchModel searchModel)
        {
            if (!(searchModel.Levels?.Any() ?? false))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("(");
            foreach (var level in searchModel.Levels)
            {
                stringBuilder.Append(stringBuilder.Length == 1
                    ? $"Level eq '{level}'"
                    : $" or Level eq '{level}'");
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}
