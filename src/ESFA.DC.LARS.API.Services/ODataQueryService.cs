using System.Linq;
using System.Text;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class ODataQueryService : IODataQueryService
    {
        public void SetLevelFilters(SearchModel searchModel, SearchParameters parameters)
        {
            bool levelFilterSelected = searchModel.Levels?.Any() ?? false;

            if (!levelFilterSelected)
            {
                return;
            }

            parameters.Filter = string.Empty;

            var builder = new StringBuilder();
            foreach (var level in searchModel.Levels)
            {
                builder.Append(parameters.Filter.Length == 0
                    ? $"Level eq '{level}'"
                    : $" or Level eq '{level}'");
            }

            parameters.Filter = builder.ToString();
        }
    }
}