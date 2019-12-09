using DFC.FindACourseClient.Models.APIResponses.CourseGet.Enums;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient.Extensions
{
    public static class CourseHoursExtensions
    {
        public static List<StudyMode> MapToStudyModes(this List<CourseHours> courseHours)
        {
            var result = new List<StudyMode>();

            foreach (var courseHour in courseHours)
            {
                switch (courseHour)
                {
                    case CourseHours.Fulltime:
                        result.Add(StudyMode.FullTime);
                        break;

                    case CourseHours.PartTime:
                        result.Add(StudyMode.PartTime);
                        break;

                    case CourseHours.Flexible:
                        result.Add(StudyMode.Flexible);
                        break;

                    case CourseHours.All:
                    default:
                        result.Add(StudyMode.Flexible);
                        result.Add(StudyMode.PartTime);
                        result.Add(StudyMode.FullTime);
                        result.Add(StudyMode.Undefined);
                        break;
                }
            }

            return result.Distinct().ToList();
        }
    }
}