using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Models;
using DFC.FindACourseClient.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DFC.FindACourseClient
{
    public class MessageConverter : IMessageConverter
    {
        internal static CultureInfo CultureInfo => new CultureInfo("en-GB");

        public CourseListInput GetCourseListRequest(string keyword, CourseSearchSvcSettings courseSearchSvcSettings)
        {
            var apiRequest = new CourseListInput
            {
                CourseListRequest = new CourseListRequestStructure
                {
                    CourseSearchCriteria = new SearchCriteriaStructure
                    {
                        APIKey = courseSearchSvcSettings?.ApiKey,
                        SubjectKeyword = keyword,
                        EarliestStartDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo),
                        AttendanceModes = courseSearchSvcSettings?.AttendanceModes?.Split(','),
                    },
                    RecordsPerPage = courseSearchSvcSettings?.SearchPageSize,
                    PageNo = "1",
                    SortBy = SortType.A,
                    SortBySpecified = true,
                },
            };

            return apiRequest;
        }

        public IEnumerable<CourseSumary> ConvertToCourse(CourseListOutput apiResult)
        {
            var result = apiResult?.CourseListResponse?.CourseDetails?.Select(c =>
                new CourseSumary
                {
                    Title = c.Course.CourseTitle,
                    Provider = c.Provider.ProviderName,
                    StartDate = Convert.ToDateTime(c.Opportunity.StartDate.Item, CultureInfo),
                    CourseId = c.Course.CourseID,
                    Location = new CourseLocation()
                    {
                        Town = (c.Opportunity.Item as VenueInfo)?.VenueAddress.Town,
                        PostCode = (c.Opportunity.Item as VenueInfo)?.VenueAddress.PostCode,
                    },
                });

            return result ?? Enumerable.Empty<CourseSumary>();
        }
    }
}
