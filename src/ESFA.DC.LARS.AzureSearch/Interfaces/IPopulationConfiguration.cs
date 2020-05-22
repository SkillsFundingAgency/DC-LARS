namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IPopulationConfiguration
    {
        string LarsConnectionString { get; }

        string LearningAimsIndexName { get; }

        string LookupsIndexName { get; }

        string FrameworkIndexName { get; set; }

        string StandardIndexName { get; set; }

        string UnitIndexName { get; set; }
    }
}