using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESFA.DC.LARS.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static IEnumerable<SelectListItem> GetEnumValueSelectList<TEnum>(this IHtmlHelper htmlHelper)
            where TEnum : struct
        {
            return new SelectList(
                Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = GetEnumText(x),
                        Value = x.ToString()
                    }),
                "Value",
                "Text");
        }

        private static string GetEnumText(Enum value)
        {
            // Use display attribute if present
            var displayText = value.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayAttribute>()?.Name;

            if (!string.IsNullOrWhiteSpace(displayText))
            {
                return displayText;
            }

            return value.ToString();
        }
    }
}
