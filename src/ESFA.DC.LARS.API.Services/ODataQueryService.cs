using System.Collections.Generic;
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
            var odataQuery = string.Empty;

            foreach (var filter in _odataFilters)
            {
                var filterString = filter.ApplyFilter(searchModel);
                odataQuery += filterString;

                if (!string.IsNullOrEmpty(filterString))
                {
                    odataQuery += Concatenation;
                }
            }

            if (!string.IsNullOrEmpty(odataQuery))
            {
                odataQuery = odataQuery.Substring(0, odataQuery.Length - Concatenation.Length);
            }

            parameters.Filter = odataQuery;
        }
    }
}