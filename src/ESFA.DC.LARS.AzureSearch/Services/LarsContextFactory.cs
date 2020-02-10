using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class LarsContextFactory : ILarsContextFactory
    {
        private readonly IPopulationConfiguration _configuration;

        public LarsContextFactory(IPopulationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LarsContext GetLarsContext()
        {
            var config = new DbContextOptionsBuilder<LarsContext>();

            config.UseSqlServer(_configuration.LarsConnectionString);

            return new LarsContext(config.Options);
        }
    }
}