﻿using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient
{
    internal static class CourseListExtensions
    {
        internal static IEnumerable<Course> SelectCoursesForJobProfile(this IEnumerable<Course> courses)
        {
            if (courses == null)
            {
                return Enumerable.Empty<Course>();
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