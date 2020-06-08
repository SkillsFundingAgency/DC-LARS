using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIssuingAuthoritySortingService
    {
        IEnumerable<IssuingAuthorityLookupModel> Sort(IEnumerable<IssuingAuthorityLookupModel> lookups);
    }
}
