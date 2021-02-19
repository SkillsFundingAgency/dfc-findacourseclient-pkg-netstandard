using System;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class AlternativeCourseRun
    {
        public string VenueName { get; set; }

        public DateTime? StartDate { get; set; }

        public string RunId { get; set; }

        public string CourseURL { get; set; }

        public string VenueUrl { get; set; }
    }
}