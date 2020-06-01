using System;
using System.Globalization;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class StringExtensions
    {
        public static string ToCurrency(this string value)
        {
            return Convert.ToDecimal(value).ToString("C0").Replace(",", string.Empty);
        }

        public static string RemovePrecision(this string value)
        {
            return decimal.Truncate(Convert.ToDecimal(value)).ToString(CultureInfo.InvariantCulture);
        }
    }
}
