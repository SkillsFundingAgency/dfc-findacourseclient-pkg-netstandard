using AutoMapper;
using DFC.FindACourseClient.UnitTests.ClientHandlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests.Services
{
    public class CourseSearchServiceGetTLevelDetailsAsyncTests
    {
        private readonly IAuditService defaultAuditService;
        private readonly ICourseSearchApiService defaultCourseSearchService;
        private readonly IMapper defaultMapper;
        private readonly Guid tLevelId = Guid.NewGuid();

        public CourseSearchServiceGetTLevelDetailsAsyncTests()
        {
            var defaultFindACourseClient = A.Fake<IFindACourseClient>();
            defaultAuditService = A.Fake<IAuditService>();
            defaultMapper = AutomapperSingleton.Mapper;
            defaultCourseSearchService = new CourseSearchApiService(defaultFindACourseClient, defaultAuditService, defaultMapper);
        }

        [Fact]
        public async Task GetTLevelDetailsAsyncWhenEmptyStringSentThenNullIsReturned()
        {
            // Act
            var result = await defaultCourseSearchService.GetTLevelDetailsAsync(string.Empty).ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseDetailsAsyncReturnsTLevelDetailsList()
        {
            // Arrange
            var dummyApiResponse = BuildTLevelResponse();
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.TLevelGetAsync(A<string>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.GetTLevelDetailsAsync(tLevelId.ToString()).ConfigureAwait(false);

            // Assert
            Assert.Equal(tLevelId.ToString(), result.TLevelId.ToString());
            Assert.Equal(nameof(TLevelProvider.ProviderName), result.ProviderDetails.Name);
        }

        private TLevelDetailResponse BuildTLevelResponse()
        {
            return new TLevelDetailResponse
            {
                TLevelId = tLevelId,
                Provider = new TLevelProvider() { ProviderName = nameof(TLevelProvider.ProviderName) },
            };
        }
    }
}