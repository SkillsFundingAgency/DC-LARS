using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureDownloadDataMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var downloadData = new DownloadDetailsModel
            {
                Id = "1",
                Version = "007",
                Type = "CSV",
                DateUploaded = DateTime.Now,
                ApplicableFrom = DateTime.Now,
                DownloadLink = "larsdownload/2020/07 Jul/09/Learning Delivery/20200709_105838_LearningDelivery_V007_CSV.Zip"
            };

            var service = new AzureDownloadDataMapper();

            var result = service.Map(downloadData);

            result.Id.Should().Be(downloadData.Id);
            result.Version.Should().Be(downloadData.Version);
            result.Type.Should().Be(downloadData.Type);
            result.DateUploaded.Should().Be(downloadData.DateUploaded);
            result.ApplicableFrom.Should().Be(downloadData.ApplicableFrom);
            result.DownloadLink.Should().Be(downloadData.DownloadLink);
        }
    }
}