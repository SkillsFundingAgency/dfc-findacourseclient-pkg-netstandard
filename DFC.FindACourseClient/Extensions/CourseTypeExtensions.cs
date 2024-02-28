using System.Collections.Generic;
using System.Linq;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    internal static class CourseTypeExtensions
    {
        internal static List<CourseType> MapToCourseTypes(this CourseType courseTypes)
        {
            var result = new List<CourseType>();
            switch (courseTypes)
            {
                case CourseType.All:
                default:
                    result.Add(CourseType.EssentialSkills);
                    result.Add(CourseType.TLevels);
                    result.Add(CourseType.HTQs);
                    result.Add(CourseType.Multiply);
                    result.Add(CourseType.FreeCoursesForJobs);
                    result.Add(CourseType.SkillsBootcamps);
                    break;
            }

            return result.Distinct().ToList();
        }

        internal static List<CourseType> MapToCompositeCourseTypes(this IList<Comp.CourseType> courseTypes)
        {
            var courseTypelist = new List<CourseType>();

            foreach (var item in courseTypes)
            {
                switch (item)
                {
                    case Comp.CourseType.Multiply:
                        courseTypelist.Add(CourseType.Multiply);
                        break;
                    case Comp.CourseType.HTQs:
                        courseTypelist.Add(CourseType.HTQs);
                        break;
                    case Comp.CourseType.FreeCoursesForJobs:
                        courseTypelist.Add(CourseType.FreeCoursesForJobs);
                        break;
                    case Comp.CourseType.TLevels:
                        courseTypelist.Add(CourseType.TLevels);
                        break;
                    case Comp.CourseType.SkillsBootcamps:
                        courseTypelist.Add(CourseType.SkillsBootcamps);
                        break;
                    case Comp.CourseType.EssentialSkills:
                        courseTypelist.Add(CourseType.EssentialSkills);
                        break;
                    case Comp.CourseType.All:
                    default:
                        break;
                }
            }

            return courseTypelist.Distinct().ToList();
        }
    }
}