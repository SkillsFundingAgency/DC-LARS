﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.LARS.API.Models
{
    public class StandardModel
    {
        public string StandardCode { get; set; }

        public string StandardName { get; set; }

        public string StandardSectorCode { get; set; }

        public string StandardSectorCodeDesc2 { get; set; }

        public int Version { get; set; }

        public string NotionalEndLevel { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? LastDateStarts { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
