using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureDownloadServiceTests
    {
        [Fact]
        public async Task GetDownloadDetails_Returns_Correct_Data()
        {
            var key = "*";

            var downloadData = PopulateDownloadsData();

            var clientMock = new Mock<IDownloadsIndexService>();

            var azureServiceMock = new Mock<IAzureService>();
            azureServiceMock
                .Setup(m => m.GetAsync<IEnumerable<DownloadDetailsModel>>(clientMock.Object, key))
                .ReturnsAsync(downloadData);

            var mapper = new AzureDownloadDataMapper();
            var service = new AzureDownloadsService(azureServiceMock.Object, mapper, clientMock.Object);

            var result = (await service.GetDownloadDetails(key)).ToList();

            result.Count.Should().Be(6);
            result[0].Id.Should().Be("4");
            result[1].Id.Should().Be("8");
            result[2].Id.Should().Be("12");
            result[3].Id.Should().Be("2");
            result[4].Id.Should().Be("6");
            result[5].Id.Should().Be("10");
        }

        private static IEnumerable<DownloadDetailsModel> PopulateDownloadsData()
        {
            var applicableFrom = DateTime.Now;
            var downloadLink = "larsdownload/2020/07 Jul/09/Learning Delivery/20200709_105838_LearningDelivery_V007_CSV.Zip";

            var csvs = new List<DownloadDetailsModel>
            {
                new DownloadDetailsModel
                {
                    Id = "1",
                    Version = "007",
                    Type = "CSV",
                    DateUploaded = new DateTime(2020, 3, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "2",
                    Version = "006",
                    Type = "CSV",
                    DateUploaded = new DateTime(2020, 4, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "3",
                    Version = "007",
                    Type = "CSV",
                    DateUploaded = new DateTime(2020, 5, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "4",
                    Version = "007",
                    Type = "CSV",
                    DateUploaded = new DateTime(2020, 5, 5),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                }
            };

            var mdbs = new List<DownloadDetailsModel>
            {
                new DownloadDetailsModel
                {
                    Id = "5",
                    Version = "006",
                    Type = "MDB",
                    DateUploaded = new DateTime(2020, 3, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "6",
                    Version = "006",
                    Type = "MDB",
                    DateUploaded = new DateTime(2020, 4, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "7",
                    Version = "007",
                    Type = "MDB",
                    DateUploaded = new DateTime(2020, 4, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "8",
                    Version = "007",
                    Type = "MDB",
                    DateUploaded = new DateTime(2020, 5, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                }
            };

            var psvs = new List<DownloadDetailsModel>
            {
                new DownloadDetailsModel
                {
                    Id = "9",
                    Version = "006",
                    Type = "PSV",
                    DateUploaded = new DateTime(2020, 3, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "10",
                    Version = "006",
                    Type = "PSV",
                    DateUploaded = new DateTime(2020, 4, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "11",
                    Version = "007",
                    Type = "PSV",
                    DateUploaded = new DateTime(2020, 4, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                },
                new DownloadDetailsModel
                {
                    Id = "12",
                    Version = "007",
                    Type = "PSV",
                    DateUploaded = new DateTime(2020, 5, 1),
                    ApplicableFrom = applicableFrom,
                    DownloadLink = downloadLink
                }
            };

            var result = new List<DownloadDetailsModel>();
            result.AddRange(csvs);
            result.AddRange(mdbs);
            result.AddRange(psvs);

            return result;
        }
    }
}