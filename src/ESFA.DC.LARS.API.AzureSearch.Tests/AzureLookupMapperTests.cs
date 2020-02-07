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
    public class AzureLookupMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var academicYearLookupMapperMock = new Mock<IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel>>();
            academicYearLookupMapperMock
                .Setup(m => m.Map(It.IsAny<AcademicYearLookupModel>()))
                .Returns(new Models.AcademicYearLookupModel());

            var nvqMapperMock = new Mock<IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model>>();
            nvqMapperMock
                .Setup(m => m.Map(It.IsAny<NotionalNVQLevel2LookupModel>()))
                .Returns(new Models.NotionalNVQLevel2Model());

            var model = new LookUpModel
            {
                LookUpKey = "1",
                NotionalNvqLevel2Lookups = new List<NotionalNVQLevel2LookupModel>
                {
                    new NotionalNVQLevel2LookupModel()
                },
                AcademicYearLookups = new List<AcademicYearLookupModel>
                {
                    new AcademicYearLookupModel()
                }
            };

            var mapper = new AzureLookupMapper(academicYearLookupMapperMock.Object, nvqMapperMock.Object);
            var result = mapper.Map(model);

            result.LookUpKey.Should().Be(model.LookUpKey);
            result.NotionalNvqLevel2Lookups.Should().HaveCount(model.NotionalNvqLevel2Lookups.Count());
            result.AcademicYearLookups.Should().HaveCount(model.AcademicYearLookups.Count());
        }
    }
}