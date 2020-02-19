namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class SubRegion
    {
        public string SubRegionId { get; set; }

        public string Name { get; set; }

        public ParentRegion ParentRegion { get; set; }
    }
}