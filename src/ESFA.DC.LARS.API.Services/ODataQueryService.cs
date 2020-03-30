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
        private readonly IEnumerable<ILearningAimsODataFilter> _learningAimOdataFilters;
        private readonly IEnumerable<IFrameworkODataFilter> _frameworkODataFilters;

        public ODataQueryService(
            IEnumerable<ILearningAimsODataFilter> learningAimOdataFilters,
            IEnumerable<IFrameworkODataFilter> frameworkODataFilters)
        {
            _learningAimOdataFilters = learningAimOdataFilters;
            _frameworkODataFilters = frameworkODataFilters;
        }

        public void SetLearningAimFilters(LearningAimsSearchModel searchModel, SearchParameters parameters)
        {
            var odataQuery = new StringBuilder();

            foreach (var filter in _learningAimOdataFilters)
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

        public void SetFrameworkFilters(FrameworkSearchModel searchModel, SearchParameters parameters)
        {
            var odataQuery = new StringBuilder();

            foreach (var filter in _frameworkODataFilters)
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