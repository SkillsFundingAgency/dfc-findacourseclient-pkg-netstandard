using System.Collections.Generic;

namespace DFC.FindACourseClientV2.Models.APIResponses
{
    public class CourseSearchResponse
    {
        public int Total { get; set; }

        public Facets Facets { get; set; }

        public IEnumerable<Result> Results { get; set; }
    }
}
