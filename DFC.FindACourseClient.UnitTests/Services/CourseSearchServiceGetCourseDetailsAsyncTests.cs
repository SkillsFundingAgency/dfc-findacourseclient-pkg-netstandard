using AutoMapper;
using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Models.APIRequests;
using DFC.FindACourseClient.Models.APIResponses.CourseGet;
using DFC.FindACourseClient.Models.APIResponses.CourseGet.Enums;
using DFC.FindACourseClient.Services;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests.Services
{
    public class CourseSearchServiceGetCourseDetailsAsyncTests
    {
        private const string ProviderName1 = "Provider1";

        private readonly IAuditService defaultAuditService;
        private readonly ICourseSearchService defaultCourseSearchService;
        private readonly IMapper defaultMapper;
        private readonly Guid courseId = Guid.NewGuid();
        private readonly Guid courseRunId = Guid.NewGuid();

        public CourseSearchServiceGetCourseDetailsAsyncTests()
        {
            var defaultFindACourseClient = A.Fake<IFindACourseClient>();
            defaultAuditService = A.Fake<IAuditService>();
            defaultMapper = A.Fake<IMapper>();
            defaultCourseSearchService = new CourseSearchService(defaultFindACourseClient, defaultAuditService, defaultMapper);
        }

        [Fact]
        public async Task GetCourseDetailsAsyncWhenEmptyStringKeywordsSentThenNullIsReturned()
        {
            // Act
            var result = await defaultCourseSearchService.GetCourseDetailsAsync(string.Empty, string.Empty).ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseDetailsAsyncReturnsGroupedCourseList()
        {
            // Arrange

            var dummyApiResponse = BuildCourseRunDetailResponse();
            var findACourseClient = A.Fake<IFindACourseClient>();
            A.CallTo(() => findACourseClient.CourseGetAsync(A<CourseGetRequest>.Ignored)).Returns(dummyApiResponse);

            var courseSearchService = new CourseSearchService(findACourseClient, defaultAuditService, defaultMapper);

            // Act
            var result = await courseSearchService.GetCourseDetailsAsync(courseId.ToString(), courseRunId.ToString()).ConfigureAwait(false);

            // Assert
            Assert.Equal(courseId.ToString(), result.CourseId);
        }

        private CourseRunDetailResponse BuildCourseRunDetailResponse()
        {
            return new CourseRunDetailResponse
            {
                Cost = 123,
                CostDescription = "CostDescription",
                CourseName = "CourseName",
                CourseRunId = courseRunId,
                CourseURL = "CourseUrl",
                CreatedDate = DateTime.UtcNow.AddYears(-2),
                DeliveryMode = DeliveryMode.ClassroomBased,
                DurationValue = 12,
                DurationUnit = DurationUnit.Months,
                FlexibleStartDate = true,
                AttendancePattern = AttendancePattern.Evening,
                StartDate = DateTime.UtcNow.AddDays(-30),
                StudyMode = StudyMode.FullTime,
                Course = new CourseDetailResponseCourse
                {
                    QualificationLevel = "QualificationLevel1",
                    CourseId = courseId,
                    HowYoullBeAssessed = "HowYoullBeAssessed",
                    WhatYoullNeed = "WhatYoullNeed",
                    AwardOrgCode = "AwardOrgCode",
                    CourseDescription = "CourseDescription",
                    EntryRequirements = "EntryRequirements",
                    AdvancedLearnerLoan = true,
                    LearnAimRef = "LearnAimRef",
                    HowYoullLearn = "HowYoullLearn",
                    WhatYoullLearn = "WhatYoullLearn",
                    WhereNext = "WhereNext",
                },
                Venue = new CourseDetailResponseVenue
                {
                    VenueName = "VenueName1",
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    Town = "Town1",
                    County = "County",
                    Postcode = "Postcode1",
                    Website = "www.coursevenue1.com",
                    Latitude = 123.0,
                    Longitude = 456.0,
                    Telephone = "Telephone",
                    Email = "Email",
                },
                Qualification = new CourseDetailResponseQualification
                {
                    QualificationLevel = 2,
                    SectorSubjectAreaTier1Desc = "SectorSubjectAreaTier1Desc",
                    SectorSubjectAreaTier2Desc = "SectorSubjectAreaTier2Desc",
                    LearnAimRefTitle = "LearnAimRefTitle",
                    LearnAimRef = "LearnAimRef",
                    AwardOrgCode = "AwardOrgCode",
                    AwardOrgName = "AwardOrgName",
                    LearnAimRefTypeDesc = "LearnAimRefTypeDesc",
                },
                Provider = new CourseDetailResponseProvider
                {
                    ProviderName = ProviderName1,
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    Town = "Town1",
                    County = "County",
                    Postcode = "Postcode1",
                    Website = "www.coursevenue1.com",
                    Telephone = "Telephone",
                    Email = "Email",
                    LearnerSatisfaction = 123,
                    EmployerSatisfaction = 345,
                    TradingName = "TradingName",
                    Alias = "Alias",
                    CourseDirectoryName = "CourseDirectoryName",
                    Fax = "Fax",
                    UKPRN = "UKPRN",
                },
                AlternativeCourseRuns = new List<CourseDetailResponseAlternativeCourseRun>
                {
                    new CourseDetailResponseAlternativeCourseRun
                    {
                        AttendancePattern = AttendancePattern.Daytime,
                        Cost = 234,
                        CostDescription = "CostDesc2",
                        CourseName = "AltCourseName2",
                        CourseRunId = Guid.NewGuid(),
                        CourseURL = "www.course2.com",
                        CreatedDate = DateTime.UtcNow.AddMonths(-3),
                        DeliveryMode = DeliveryMode.Online,
                        DurationUnit = DurationUnit.Hours,
                        DurationValue = 5,
                        FlexibleStartDate = false,
                        StartDate = DateTime.UtcNow.AddDays(-4),
                        StudyMode = StudyMode.FullTime,
                        Venue = new CourseDetailResponseVenue
                        {
                            VenueName = "VenueName2",
                            AddressLine1 = "AddressLine1_2",
                            AddressLine2 = "AddressLine2_2",
                            Town = "Town1_2",
                            County = "County_2",
                            Postcode = "Postcode1_2",
                            Website = "www.coursevenue1.com_2",
                            Latitude = 123.0,
                            Longitude = 456.0,
                            Telephone = "Telephone_2",
                            Email = "Email_2",
                        },
                    },
                },
            };
        }
    }
}