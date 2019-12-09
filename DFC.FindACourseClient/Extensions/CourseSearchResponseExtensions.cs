using DFC.FindACourseClient.Models.APIResponses.CourseSearch;
using DFC.FindACourseClient.Models.ExternalInterfaceModels;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient.Extensions
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
                        Distance = float.Parse(c.Distance ?? "0"),
                        LocationAddress = c.VenueAddress,
                    },
                    ProviderName = c.ProviderName,
                    StartDate = c.StartDate.GetValueOrDefault(),
                    StartDateLabel = "Start date:",
                    AttendanceMode = c.DeliveryModeDescription,
                    AttendancePattern = c.VenueAttendancePatternDescription,
                    QualificationLevel = !string.IsNullOrWhiteSpace(c.QualificationLevel) ? Enum.Parse(typeof(QualificationLevel), c.QualificationLevel).ToString() : string.Empty,
                    StudyMode = c.VenueStudyModeDescription,
                    Location = c.VenueTown,
                    CourseLink = $"{CourseDetailsPage}?{nameof(CourseDetails.CourseId)}={c.CourseId}",
                    Duration = $"{c.DurationValue} {c.DurationUnit.ToString()}",
                });

            return result ?? Enumerable.Empty<Course>();
        }
    }
}