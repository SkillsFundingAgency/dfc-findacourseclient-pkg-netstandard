using DFC.FindACourseClient;
using System.Collections.Generic;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseSearchResult
    {
        public IEnumerable<Course> Courses { get; set; }

        public CourseSearchResultProperties ResultProperties { get; set; } = new CourseSearchResultProperties();

        public List<string> AttachedSectorIds { get; set; }
    }
}