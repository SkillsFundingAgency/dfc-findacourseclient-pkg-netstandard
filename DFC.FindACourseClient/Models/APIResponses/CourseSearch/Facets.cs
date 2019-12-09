using System.Collections.Generic;

namespace DFC.FindACourseClient.Models.APIResponses.CourseSearch
{
    public class Facets
    {
        public List<Region> Region { get; set; }

        public List<ProviderName> ProviderName { get; set; }

        public List<VenueAttendancePattern> VenueAttendancePattern { get; set; }

        public List<NotionalNVQLevelv2> NotionalNVQLevelv2 { get; set; }
    }
}