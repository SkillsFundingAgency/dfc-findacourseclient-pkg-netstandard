using System.Collections.Generic;
using System.Linq;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    internal static class CourseTypeExtensions
    {
        internal static List<CourseType> MapToCourseTypes(this CourseType courseType)
        {
            var result = new List<CourseType>();
            switch (courseType)
            {
                case CourseType.EssentialSkills:
                    result.Add(CourseType.EssentialSkills);
                    break;

                case CourseType.TLevels:
                    result.Add(CourseType.TLevels);
                    break;

                case CourseType.HTQs:
                    result.Add(CourseType.HTQs);
                    break;

                case CourseType.FreeCoursesForJobs:
                    result.Add(CourseType.FreeCoursesForJobs);
                    break;

                case CourseType.Multiply:
                    result.Add(CourseType.Multiply);
                    break;

                case CourseType.SkillsBootcamp:
                    result.Add(CourseType.SkillsBootcamp);
                    break;


                case CourseType.All:
                default:
                    result.Add(CourseType.EssentialSkills);
                    result.Add(CourseType.TLevels);
                    result.Add(CourseType.HTQs);
                    result.Add(CourseType.FreeCoursesForJobs);
                    result.Add(CourseType.Multiply);
                    result.Add(CourseType.SkillsBootcamp);
                    break;
            }

            return result.Distinct().ToList();
        }
    }
}