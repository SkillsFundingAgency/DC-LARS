using System.Collections.Generic;
using System.Text;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class ODataQueryService : IODataQueryService
    {
        private const string Concatenation = " and ";
        private readonly IEnumerable<IODataFilter> _odataFilters;

        public ODataQueryService(IEnumerable<IODataFilter> odataFilters)
        {
            _odataFilters = odataFilters;
        }

        public void SetFilters(SearchModel searchModel, SearchParameters parameters)
        {
            var odataQuery = new StringBuilder();

            foreach (var filter in _odataFilters)
            {
                var filterString = filter.ApplyFilter(searchModel);

                if (odataQuery.Length != 0 && !string.IsNullOrEmpty(filterString))
                {
                    odataQuery.Append(Concatenation);
                }

                odataQuery.Append(filterString);
            }

            parameters.Filter = odataQuery.ToString();
        }
    }
}