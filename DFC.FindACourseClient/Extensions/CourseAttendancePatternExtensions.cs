using System;
using System.Collections.Generic;

namespace DFC.FindACourseClient
{
    internal static class CourseAttendancePatternExtensions
    {
        internal static List<int> MapToCompositeAttendancePattern(this IList<AttendancePattern> attendancePatterns)
        {
            var attenancePatternList = new List<int>();

            foreach (var item in attendancePatterns)
            {
                switch (item)
                {
                    case AttendancePattern.Daytime:
                        attenancePatternList.Add((int)AttendancePattern.Daytime);
                        break;

                    case AttendancePattern.Evening:
                        attenancePatternList.Add((int)AttendancePattern.Evening);
                        break;

                    case AttendancePattern.Weekend:
                        attenancePatternList.Add((int)AttendancePattern.Weekend);
                        break;

                    case AttendancePattern.DayOrBlockRelease:
                        attenancePatternList.Add((int)AttendancePattern.DayOrBlockRelease);
                        break;

                    default:
                        break;
                }
            }

            return attenancePatternList;
        }
    }
}
