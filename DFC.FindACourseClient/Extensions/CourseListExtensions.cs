using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient
{
    internal static class CourseListExtensions
    {
        internal static IEnumerable<CourseOrTlevel> SelectCoursesForJobProfile(this IEnumerable<CourseOrTlevel> courses)
        {
            if (courses == null)
            {
                return Enumerable.Empty<CourseOrTlevel>();
            }

            if (courses.Count() > 2)
            {
                var distinctProviders = courses.Select(c => c.ProviderName).Distinct().Count();
                if (distinctProviders > 1)
                {
                    return courses
                        .GroupBy(c => c.ProviderName)
                        .Select(g => g.First())
                        .Take(2);
                }
            }

            return courses.Take(2);
        }
    }
}