﻿using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class FrameworkSearchModel
    {
        public string SearchTerm { get; set; }

        public List<string> FrameworkTypes { get; set; }

        public List<string> IssuingAuthorities { get; set; }
    }
}