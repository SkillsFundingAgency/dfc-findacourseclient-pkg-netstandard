using System.Collections.Generic;

namespace DFC.FindACourseClientV2.Models.APIResponses.CourseSearch
{
    public class CourseSearchResponse
    {
        public int Total { get; set; }

        public int Start { get; set; }

        public int Limit { get; set; }

        public Facets Facets { get; set; }

        public IEnumerable<Result> Results { get; set; }
    }
}