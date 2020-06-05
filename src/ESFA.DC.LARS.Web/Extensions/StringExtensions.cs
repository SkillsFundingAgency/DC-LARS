using System;
using System.Globalization;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class StringExtensions
    {
        private const string CultureNameGB = "en-gb";

        private static CultureInfo CultureInfo
        {
            get
            {
                var culture = CultureInfo.CreateSpecificCulture(CultureNameGB);
                culture.NumberFormat.CurrencyGroupSeparator = string.Empty;
                return culture;
            }
        }

        public static string ToCurrency(this string value)
        {
            return Convert.ToDecimal(value).ToString("C0", CultureInfo);
        }

        public static string RemovePrecision(this string value)
        {
            return decimal.Truncate(Convert.ToDecimal(value)).ToString(CultureInfo.InvariantCulture);
        }
    }
}
