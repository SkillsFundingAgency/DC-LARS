using System;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class DateTimeExtensions
    {
        private const string DateTimeStringFormat = "dd MMMM yyyy";

        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString(DateTimeStringFormat);
        }

        public static string ToDateStringOrDefault(this DateTime? dateTime, string defaultValue)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString(DateTimeStringFormat);
            }

            return defaultValue;
        }
    }
}
