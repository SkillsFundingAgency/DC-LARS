using System;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMMM yyyy");
        }
    }
}
