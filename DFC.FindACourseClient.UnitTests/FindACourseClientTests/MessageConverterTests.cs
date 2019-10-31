using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Models;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.Models.CosmosDb;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests
{
    [Trait("Course Search Client", "Tests")]
    public class MessageConverterTests
    {
        private readonly DateTime startDateTimeForCourses;

        public MessageConverterTests()
        {
            startDateTimeForCourses = DateTime.Now;
        }

        [Fact]
        public void ConvertToCourse()
        {
            //Arrange
            int numberCourses = 2;
            var expectedCourses = GetTestCourseSearchResponse(numberCourses);

            //Act
            var messageConverter = new MessageConverter();
            var result = messageConverter.ConvertToCourse(expectedCourses);

            //Assert
            result.Count().Should().Be(numberCourses);

            int index = 0;
            foreach (CourseSumary c in result)
            {
                var expectedCourse = expectedCourses.CourseListResponse.CourseDetails[index++];
                c.Title.Should().Be(expectedCourse.Course.CourseTitle);
                c.Provider.Should().Be(expectedCourse.Provider.ProviderName);
                c.StartDate.Date.Should().Be(startDateTimeForCourses.Date);
                c.CourseId.Should().Be(expectedCourse.Course.CourseID);
                c.Location.Town.Should().Be((expectedCourse.Opportunity.Item as VenueInfo)?.VenueAddress.Town);
                c.Location.PostCode.Should().Be((expectedCourse.Opportunity.Item as VenueInfo)?.VenueAddress.PostCode);
            }
        }

        [Fact]
        public void GetCourseListRequest()
        {
            //Arrange
            var courseSearchSvcSettings = new CourseSearchSvcSettings()
            {
                ApiKey = "ApiKey",
                AttendanceModes = "A1,A2",
                SearchPageSize = "5",
            };

            //Act
            var messageConverter = new MessageConverter();
            var result = messageConverter.GetCourseListRequest("KeyWord", courseSearchSvcSettings).CourseListRequest;

            //Assert
            result.CourseSearchCriteria.APIKey.Should().Be(courseSearchSvcSettings.ApiKey);
            result.CourseSearchCriteria.AttendanceModes.Should().BeEquivalentTo(courseSearchSvcSettings.AttendanceModes?.Split(','));
            result.RecordsPerPage.Should().Be(courseSearchSvcSettings.SearchPageSize);
        }

        private CourseListOutput GetTestCourseSearchResponse(int numberCourses)
        {
            var courseListOutput = new CourseListOutput()
            {
                CourseListResponse = new CourseListResponseStructure()
                {
                    CourseDetails = GetTestCourses(numberCourses),
                },
            };

            return courseListOutput;
        }

        private CourseStructure[] GetTestCourses(int numberCourses)
        {
            var courseList = new List<CourseStructure>();

            for (int ii = 0; ii < numberCourses; ii++)
            {
                var course = new CourseStructure()
                {
                    Provider = new ProviderInfo()
                    {
                        ProviderName = $"Test Provider {ii}",
                    },

                    Course = new CourseInfo()
                    {
                        CourseID = $"{ii}",
                        CourseTitle = $"CourseTitle{ii}",
                    },

                    Opportunity = new OpportunityInfo()
                    {
                        StartDate = new StartDateType()
                        {
                            Item = startDateTimeForCourses.ToString(new CultureInfo("en-GB")),
                        },
                        Item = new VenueInfo()
                        {
                            VenueAddress = new AddressType()
                            {
                                Town = $"TestTown{ii}",
                                PostCode = $"PC{ii}",
                            },
                        },
                    },
                };

                courseList.Add(course);
            }

            return courseList.ToArray();
        }
    }
}
