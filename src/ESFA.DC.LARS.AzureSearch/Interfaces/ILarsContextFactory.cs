using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface ILarsContextFactory
    {
        LarsContext GetLarsContext();
    }
}