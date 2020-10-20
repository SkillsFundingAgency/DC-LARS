using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class DowloadDataProviderService : IDownloadDataProviderService
    {
        private const string CSVTypeName = "CSV";
        private const string PSVTypeName = "PSV";
        private const string MDBTypeName = "MDB";

        private readonly IPopulationConfiguration _configuration;
        private readonly ILarsContextFactory _contextFactory;

        public DowloadDataProviderService(
            IPopulationConfiguration configuration,
            ILarsContextFactory contextFactory)
        {
            _configuration = configuration;
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<DownloadDetailsModel>> GetDownloadDetails(CancellationToken cancellationToken)
        {
            var result = new List<DownloadDetailsModel>();

            List<LarsVersion> versions;
            using (var context = _contextFactory.GetLarsContext())
            {
                versions = await context.LarsVersions
                    .OrderByDescending(v => v.MajorNumber)
                    .Take(2)
                    .ToListAsync(cancellationToken);
            }

            var id = 0;
            foreach (var version in versions)
            {
                result.Add(CreateDownloadDetailsModel(version, CSVTypeName, ref id));
                result.Add(CreateDownloadDetailsModel(version, MDBTypeName, ref id));
                result.Add(CreateDownloadDetailsModel(version, PSVTypeName, ref id));
            }

            return result;
        }

        private DownloadDetailsModel CreateDownloadDetailsModel(LarsVersion version, string type, ref int id)
        {
            var formattedVersion = version.MajorNumber.ToString().PadLeft(3, '0');
            var csv = new DownloadDetailsModel
            {
                Id = (++id).ToString(),
                Version = version.MajorNumber.ToString(),
                Type = type,
                ApplicableFrom = version.ActivationDate,
                DateUploaded = version.DateUploaded,
                DownloadLink = $"published/{formattedVersion}/LearningDelivery_V{formattedVersion}_{type}.Zip"
            };
            return csv;
        }
    }
}