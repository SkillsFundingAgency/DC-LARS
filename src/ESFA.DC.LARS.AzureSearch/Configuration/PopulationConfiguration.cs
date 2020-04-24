using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Configuration
{
    public class PopulationConfiguration : IPopulationConfiguration
    {
        public string LarsConnectionString { get; set; }

        public string AzureWebJobsDashboard { get; set; }

        public string AzureWebJobsStorage { get; set; }

        public string AzureWebJobsServiceBus { get; set; }

        public string LearningAimsIndexName { get; set; }

        public string LookupsIndexName { get; set; }

        public string FrameworkIndexName { get; set; }

        public string UnitIndexName { get; set; }
    }
}