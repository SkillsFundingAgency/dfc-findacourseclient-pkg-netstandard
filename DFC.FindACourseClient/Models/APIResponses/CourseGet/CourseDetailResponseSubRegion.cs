namespace DFC.FindACourseClient
{
    public class CourseDetailResponseSubRegion
    {
        public string SubRegionId { get; set; }

        public string Name { get; set; }

        public CourseDetailResponseParentRegion ParentRegion { get; set; }
    }
}