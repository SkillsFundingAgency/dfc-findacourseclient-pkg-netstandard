﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    public interface ICourseSearchApiService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string jobProfileKeywords);

        Task<CourseSearchResult> SearchCoursesAsync(CourseSearchProperties courseSearchProperties);

        Task<CourseDetails> GetCourseDetailsAsync(string courseId, string oppurtunityId);
    }
}