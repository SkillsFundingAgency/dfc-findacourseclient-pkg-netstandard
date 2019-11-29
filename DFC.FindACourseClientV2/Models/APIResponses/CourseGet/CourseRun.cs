using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClientV2.Models.APIResponses
{
    public class CourseRun
    {
        public string Id { get; set; }

        public int CourseInstanceId { get; set; }

        public string VenueId { get; set; }

        public string CourseName { get; set; }

        public string ProviderCourseID { get; set; }

        public int DeliveryMode { get; set; }

        public bool FlexibleStartDate { get; set; }

        public DateTime? StartDate { get; set; }

        public string CourseURL { get; set; }

        public double Cost { get; set; }

        public string CostDescription { get; set; }

        public int DurationUnit { get; set; }

        public int DurationValue { get; set; }

        public int StudyMode { get; set; }

        public int AttendancePattern { get; set; }

        public IList<string> Regions { get; set; }

        public int RecordStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
    }
}
