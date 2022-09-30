using System.Collections.Generic;

namespace DFC.FindACourseClient
{
    public class CourseSearchResult
    {
        public IEnumerable<CourseOrTlevel> Courses { get; set; }

        public CourseSearchResultProperties ResultProperties { get; set; } = new CourseSearchResultProperties();
    }
}