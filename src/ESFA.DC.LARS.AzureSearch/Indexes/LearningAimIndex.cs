using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Indexes
{
    public class LearningAimIndex : IIndex
    {
        public SearchIndexes IndexName => SearchIndexes.LearningDeliveryIndex;
    }
}