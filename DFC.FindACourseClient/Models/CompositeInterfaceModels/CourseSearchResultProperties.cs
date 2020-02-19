namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseSearchResultProperties
    {
        public int Page { get; set; }

        public CourseSearchOrderBy OrderedBy { get; set; }

        public int TotalResultCount { get; set; }

        public int TotalPages { get; set; }
    }
}