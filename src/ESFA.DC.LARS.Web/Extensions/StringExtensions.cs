using System;
using System.Globalization;
using System.Threading;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class StringExtensions
    {
        private const string CultureNameGB = "en-gb";

        public static string ToCurrency(this string value)
        {
            var culture = CultureInfo.CreateSpecificCulture(CultureNameGB);
            return ToCurrency(value, culture);
        }

        public static string ToCurrency(this string value, CultureInfo culture)
        {
            culture.NumberFormat.CurrencyGroupSeparator = string.Empty;
            return Convert.ToDecimal(value).ToString("C0", culture);
        }

        public static string RemovePrecision(this string value)
        {
            return decimal.Truncate(Convert.ToDecimal(value)).ToString(CultureInfo.InvariantCulture);
        }
    }
}
