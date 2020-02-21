﻿using System.Linq;
using System.Text;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class FundingStreamODataFilter : IODataFilter
    {
        private static string FilterODataString => "AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{0}') and ay/AcademicYear eq '{1}')";

        public string ApplyFilter(SearchModel searchModel)
        {
            var searchFilter = searchModel.FundingStreams;

            if (!(searchFilter?.Any() ?? false) || searchFilter.All(string.IsNullOrWhiteSpace))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("(");
            foreach (var filter in searchFilter)
            {
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Append(" or ");
                }

                stringBuilder.Append(string.Format(FilterODataString, filter, searchModel.TeachingYears.FirstOrDefault()));
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}