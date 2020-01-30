using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Indexes
{
    public class LookupIndex : IIndex
    {
        public SearchIndexes IndexName => SearchIndexes.LookUpIndex;
    }
}