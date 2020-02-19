using System.Collections.Generic;
using System.Linq;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

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

        internal static List<StudyMode> MapToCompositeStudyModes(this IList<Comp.CourseHours> courseHours)
        {
            var studyModeList = new List<StudyMode>();

            foreach (var item in courseHours)
            {
                switch (item)
                {
                    case Comp.CourseHours.Fulltime:
                        studyModeList.Add(StudyMode.FullTime);
                        break;

                    case Comp.CourseHours.PartTime:
                        studyModeList.Add(StudyMode.PartTime);
                        break;

                    case Comp.CourseHours.Flexible:
                        studyModeList.Add(StudyMode.Flexible);
                        break;

                    case Comp.CourseHours.All:
                    default:
                        studyModeList.Add(StudyMode.Flexible);
                        studyModeList.Add(StudyMode.PartTime);
                        studyModeList.Add(StudyMode.FullTime);
                        studyModeList.Add(StudyMode.Undefined);
                        break;
                }
            }

            return studyModeList.Distinct().ToList();
        }
    }
}