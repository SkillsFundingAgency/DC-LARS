using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureAcademicYearMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var validityModel = new ValidityModel();
            var fundingModel = new FundingModel();

            var validityMapperMock = new Mock<IMapper<ValidityModel, Models.ValidityModel>>();
            validityMapperMock.Setup(m => m.Map(validityModel)).Returns(new Models.ValidityModel());

            var fundingMapperMock = new Mock<IMapper<FundingModel, Models.FundingModel>>();
            fundingMapperMock.Setup(m => m.Map(fundingModel)).Returns(new Models.FundingModel());

            var academicYear = new AcademicYearModel
            {
                AcademicYear = "testYear",
                Validities = new List<ValidityModel>
                {
                    validityModel
                },
                Fundings = new List<FundingModel>
                {
                    fundingModel
                }
            };

            var mapper = new AzureAcademicYearMapper(fundingMapperMock.Object, validityMapperMock.Object);

            var result = mapper.Map(academicYear);

            result.AcademicYear.Should().Be("testYear");

            result.Fundings.Should().HaveCount(1);
            result.Fundings.Single().Should().BeEquivalentTo(fundingModel);

            result.Validities.Should().HaveCount(1);
            result.Validities.Single().Should().BeEquivalentTo(validityModel);
        }
    }
}