using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESFA.DC.LARS.Web.Helpers
{
    public static class EnumHelper
    {
        public static List<SelectListItem> EnumToSelectList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = v.ToString()
            }).ToList();
        }
    }
}
