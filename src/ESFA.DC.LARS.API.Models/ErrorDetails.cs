using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.LARS.API.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
