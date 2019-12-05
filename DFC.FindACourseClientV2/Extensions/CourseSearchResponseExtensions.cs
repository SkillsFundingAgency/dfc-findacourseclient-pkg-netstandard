using DFC.FindACourseClientV2.Models.APIResponses.CourseSearch;
using DFC.FindACourseClientV2.Models.ExternalInterfaceModels;
using DFC.FindACourseClientV2.Models.ExternalInterfaceModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClientV2.Extensions
{
    public static class CourseSearchResponseExtensions
    {
        public static IEnumerable<Course> ConvertToSearchCourse(this CourseSearchResponse apiResult)
        {
            const string CourseDetailsPage = "/find-a-course/course-details";

            var result = apiResult?.Results?.Select(c =>
                new Course
                {
                    CourseId = c.CourseId.ToString(),
                    Title = c.QualificationCourseTitle,
                    LocationDetails = new LocationDetails
                    {
                        Distance = float.Parse(c.Distance),
                        LocationAddress = c.VenueAddress,
                    },
                    ProviderName = c.ProviderName,
                    StartDate = c.StartDate.GetValueOrDefault(),
                    StartDateLabel = "Start date:",
                    AttendanceMode = c.DeliveryModeDescription,
                    AttendancePattern = c.VenueAttendancePatternDescription,
                    QualificationLevel = Enum.Parse(typeof(QualificationLevel), c.QualificationLevel).ToString(),
                    StudyMode = c.VenueStudyModeDescription,
                    Location = c.VenueTown,
                    CourseLink = $"{CourseDetailsPage}?{nameof(CourseDetails.CourseId)}={c.CourseId}",
                    Duration = $"{c.DurationValue} {c.DurationUnit.ToString()}",
                });

            return result ?? Enumerable.Empty<Course>();
        }
    }
}