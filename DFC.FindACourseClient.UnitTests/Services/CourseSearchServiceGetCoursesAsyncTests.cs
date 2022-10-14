using AutoMapper;
using DFC.FindACourseClient.UnitTests.ClientHandlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests.Services
{
    public class CourseSearchServiceGetCoursesAsyncTests
    {
        private const string ProviderName1 = "Provider1";
        private const string ProviderName2 = "Provider2";

        private readonly IAuditService defaultAuditService;
        private readonly ICourseSearchApiService defaultCourseSearchService;
        private readonly IMapper defaultMapper;

        public CourseSearchServiceGetCoursesAsyncTests()
        {
            var defaultFindACourseClient = A.Fake<IFindACourseClient>();
            defaultAuditService = A.Fake<IAuditService>();
            defaultMapper = AutomapperSingleton.Mapper;
            defaultCourseSearchService = new CourseSearchApiService(defaultFindACourseClient, defaultAuditService, defaultMapper);
        }

        [Fact]
        public async Task GetCoursesAsyncWhenEmptyStringKeywordsSentThenNullIsReturned()
        {
            // Act
            var result = await defaultCourseSearchService.GetCoursesAsync(string.Empty).ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCoursesAsyncReturnsGroupedCourseList()
        {
            // Arrange
            var dummyApiResponse = BuildCourseSearchResponse();
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.GetCoursesAsync("SomeKeyword").ConfigureAwait(false);
            var resultList = result.ToList();

            // Assert
            Assert.Equal(dummyApiResponse.Results?.FirstOrDefault()?.CourseId.ToString(), resultList.FirstOrDefault()?.CourseId);
            Assert.True(resultList.Count == 2);
        }

        [Fact]
        public async Task GetCoursesAsyncReturnsTwoCoursesAndEachFromDifferentProvider()
        {
            // Arrange
            var dummyApiResponse = BuildCourseSearchResponse();
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.GetCoursesAsync("SomeKeyword").ConfigureAwait(false);
            var resultList = result.ToList();

            // Assert
            Assert.True(resultList.Count == 2);
            Assert.Equal(ProviderName1, resultList[0].ProviderName);
            Assert.Equal(ProviderName2, resultList[1].ProviderName);
        }

        [Fact]
        public async Task GetCoursesAsyncReturnsTLevelList()
        {
            // Arrange
            var dummyApiResponse = BuildTLevelSearchResponse();
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.GetCoursesAsync("SomeKeyword").ConfigureAwait(false);
            var resultList = result.ToList();

            // Assert
            Assert.Equal(dummyApiResponse.Results?.FirstOrDefault()?.TLevelId.ToString(), resultList.FirstOrDefault()?.TLevelId);
            Assert.True(resultList.Count == 2);
        }

        [Fact]
        public async Task GetCoursesAsyncWritesToAuditLogWhenExceptionIsThrown()
        {
            // Arrange
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Throws<Exception>();

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.GetCoursesAsync("SomeKeyword").ConfigureAwait(false);

            // Assert
            A.CallTo(() => defaultAuditService.CreateAudit(null, null, null)).WithAnyArguments().MustHaveHappenedOnceExactly();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCoursesAsyncThrownExceptionsWhenRequestd()
        {
            // Arrange
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Throws<Exception>();

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            Task Result() => courseSearchService.GetCoursesAsync("SomeKeyword", true);

            // Assert
            await Assert.ThrowsAsync<Exception>(Result).ConfigureAwait(false);
        }

        private CourseSearchResponse BuildCourseSearchResponse(int startItem = 1, int totalItems = 123, int limit = 10)
        {
            return new CourseSearchResponse
            {
                Start = startItem,
                Total = totalItems,
                Limit = limit,
                Results = new List<Result>
                {
                    new Result
                    {
                        CourseDescription = "Course Desc 1",
                        CourseId = Guid.NewGuid(),
                        CourseRunId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                    },
                    new Result
                    {
                        CourseDescription = "Course Desc 2",
                        CourseId = Guid.NewGuid(),
                        CourseRunId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                    },
                    new Result
                    {
                        CourseDescription = "Course Desc 3",
                        CourseId = Guid.NewGuid(),
                        CourseRunId = Guid.NewGuid(),
                        ProviderName = ProviderName2,
                    },
                    new Result
                    {
                        CourseDescription = "Course Desc 4",
                        CourseId = Guid.NewGuid(),
                        CourseRunId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                    },
                },
            };
        }

        private CourseSearchResponse BuildTLevelSearchResponse(int startItem = 1, int totalItems = 123, int limit = 10)
        {
            return new CourseSearchResponse
            {
                Start = startItem,
                Total = totalItems,
                Limit = limit,
                Results = new List<Result>
                {
                    new Result
                    {
                        CourseDescription = "TLevel Course Desc 1",
                        TLevelId = Guid.NewGuid(),
                        TLevelLocationId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                    },
                    new Result
                    {
                        CourseDescription = "TLevel Course Desc 2",
                        TLevelId = Guid.NewGuid(),
                        TLevelLocationId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                    },
                    new Result
                    {
                        CourseDescription = "TLevel Course Desc 3",
                        TLevelId = Guid.NewGuid(),
                        TLevelLocationId = Guid.NewGuid(),
                        ProviderName = ProviderName2,
                    },
                },
            };
        }
    }
}