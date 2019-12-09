using System.Collections.Generic;

namespace DFC.FindACourseClient.Models.ExternalInterfaceModels
{
    public class CourseSearchResult
    {
        public IEnumerable<Course> Courses { get; set; }

        public CourseSearchResultProperties ResultProperties { get; set; } = new CourseSearchResultProperties();
    }
}