using System;
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

        private readonly ILarsContextFactory _contextFactory;

        public DowloadDataProviderService(ILarsContextFactory contextFactory)
        {
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

        private static DownloadDetailsModel CreateDownloadDetailsModel(LarsVersion version, string type, ref int id)
        {
            var formattedVersion = version.MajorNumber.ToString().PadLeft(3, '0');
            var csv = new DownloadDetailsModel
            {
                Id = (++id).ToString(),
                Version = version.MajorNumber.ToString(),
                Type = type,
                ApplicableFrom = version.ActivationDate,
                DateUploaded = version.ActivationDate, // TODO replace with new date when schema updated
                DownloadLink = $"larsdownloads/published/{formattedVersion}/LearningDelivery_V{formattedVersion}_{type}.Zip"
            };
            return csv;
        }
    }
}