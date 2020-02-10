namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexPopulationService
    {
        void PopulateIndex();

        void CreateIndex();

        void DeleteIndex();
    }
}