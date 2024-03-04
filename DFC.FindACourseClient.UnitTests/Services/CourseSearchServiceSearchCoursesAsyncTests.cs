using AutoMapper;
using DFC.FindACourseClient.UnitTests.ClientHandlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests.Services
{
    public class CourseSearchServiceSearchCoursesAsyncTests
    {
        private const string ProviderName1 = "Provider1";
        private const string ProviderName2 = "Provider2";

        private readonly IAuditService defaultAuditService;
        private readonly ICourseSearchApiService defaultCourseSearchService;
        private readonly IMapper defaultMapper;

        public CourseSearchServiceSearchCoursesAsyncTests()
        {
            var defaultFindACourseClient = A.Fake<IFindACourseClient>();
            defaultAuditService = A.Fake<IAuditService>();
            defaultMapper = AutomapperSingleton.Mapper;
            defaultCourseSearchService = new CourseSearchApiService(defaultFindACourseClient, defaultAuditService, defaultMapper);
        }

        [Fact]
        public async Task SearchCoursesAsyncWhenEmptyStringKeywordsSentThenNullIsReturned()
        {
            // Arrange
            var courseSearchProperties = new CourseSearchProperties
            {
                Filters = new CourseSearchFilters { SearchTerm = string.Empty },
            };

            // Act
            var result = await defaultCourseSearchService.SearchCoursesAsync(courseSearchProperties).ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SearchCoursesAsyncReturnsGroupedCourseList()
        {
            // Arrange
            var request = BuildCourseSearchProperties();
            var dummyApiResponse = BuildCourseSearchResponse();
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.SearchCoursesAsync(request).ConfigureAwait(false);

            // Assert
            Assert.True(result.Courses.Count() == 4);
        }

        [Fact]
        public async Task SearchCoursesAsyncReturnsCorrectNumberOfPages()
        {
            const int totalResults = 501;
            const int pageSize = 50;
            const int startItem = 201;
            const int expectedNumberOfPages = 11;
            const int expectedPageNumber = 6;

            var request = BuildCourseSearchProperties();
            var dummyApiResponse = BuildCourseSearchResponse(startItem, totalResults, pageSize);

            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.SearchCoursesAsync(request).ConfigureAwait(false);

            // Assert
            Assert.Equal(totalResults, result.ResultProperties.TotalResultCount);
            Assert.Equal(expectedNumberOfPages, result.ResultProperties.TotalPages);
            Assert.Equal(expectedPageNumber, result.ResultProperties.Page);
        }

        [Fact]
        public async Task SearchCoursesAsyncReturnsCorrectStartDateLabel()
        {
            var request = BuildCourseSearchProperties();
            var dummyApiResponse = BuildCourseSearchResponse();

            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseSearchAsync(A<CourseSearchRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchApiService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.SearchCoursesAsync(request).ConfigureAwait(false);

            // Assert
            Assert.Equal(DateTime.UtcNow.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture), result.Courses.ElementAt(0).StartDateLabel);
            Assert.Equal("Flexible", result.Courses.ElementAt(1).StartDateLabel);
            Assert.Null(result.Courses.ElementAt(2).StartDateLabel);
        }

        private CourseSearchProperties BuildCourseSearchProperties()
        {
            return new CourseSearchProperties
            {
                Count = 4,
                Filters = new CourseSearchFilters
                {
                    StartDate = StartDate.Anytime,
                    Distance = 123,
                    Provider = ProviderName1,
                    PostCode = "P12 1PP",
                    Town = "TestTown",
                    StartDateFrom = DateTime.UtcNow.AddYears(-1),
                    SearchTerm = "Keyword",
                    DistanceSpecified = true,
                    CourseHours = CourseHours.Flexible,
                    LearningMethod = LearningMethod.All,
                },
                Page = 3,
                OrderedBy = CourseSearchOrderBy.Relevance,
            };
        }

        private CourseSearchResponse BuildCourseSearchResponse(int startItem = 1, int totalItems = 123, int limit = 10)
        {
            return new CourseSearchResponse
            {
                Start = startItem,
                Total = totalItems,
                Limit = limit,
                Facets = new Facets
                {
                    ProviderName = new List<ProviderName> { new ProviderName { Count = 1, Value = ProviderName1 } },
                    VenueAttendancePattern = new List<VenueAttendancePattern>
                    {
                        new VenueAttendancePattern { Count = 1, Value = "VenueAttendancePattern1" },
                    },
                    NotionalNVQLevelv2 = new List<NotionalNVQLevelv2> { new NotionalNVQLevelv2 { Count = 1, Value = "NotionalNVQLevelv2" } },
                    Region = new List<Region> { new Region { Count = 1, Value = "Region1" } },
                    SectorId = new List<Facet> { new Facet { Count = 1, Value = "1" }, new Facet { Count = 1, Value = "2" } },
                },
                Results = new List<Result>
                {
                    new Result
                    {
                        CourseDescription = "Course Desc 1",
                        CourseId = Guid.NewGuid(),
                        CourseRunId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                        FlexibleStartDate = false,
                        StartDate = DateTime.UtcNow,
                    },
                    new Result
                    {
                        CourseDescription = "Course Desc 2",
                        CourseId = Guid.NewGuid(),
                        CourseRunId = Guid.NewGuid(),
                        ProviderName = ProviderName1,
                        FlexibleStartDate = true,
                        StartDate = DateTime.UtcNow,
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
    }
}