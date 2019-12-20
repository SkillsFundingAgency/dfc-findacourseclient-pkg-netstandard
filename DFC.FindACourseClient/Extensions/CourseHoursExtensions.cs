using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient
{
    internal static class CourseHoursExtensions
    {
        internal static List<StudyMode> MapToStudyModes(this CourseHours courseHours)
        {
            var result = new List<StudyMode>();

            switch (courseHours)
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

            return result.Distinct().ToList();
        }
    }
}