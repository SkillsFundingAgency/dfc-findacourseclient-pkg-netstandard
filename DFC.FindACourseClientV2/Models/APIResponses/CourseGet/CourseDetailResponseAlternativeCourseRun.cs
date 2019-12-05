using DFC.FindACourseClientV2.Models.APIResponses.CourseGet.Enums;
using System;

namespace DFC.FindACourseClientV2.Models.APIResponses.CourseGet
{
    public class CourseDetailResponseAlternativeCourseRun
    {
        public Guid CourseRunId { get; set; }

        public AttendancePattern AttendancePattern { get; set; }

        public decimal? Cost { get; set; }

        public string CostDescription { get; set; }

        public string CourseName { get; set; }

        public string CourseURL { get; set; }

        public DateTime CreatedDate { get; set; }

        public DeliveryMode DeliveryMode { get; set; }

        public DurationUnit DurationUnit { get; set; }

        public int? DurationValue { get; set; }

        public bool FlexibleStartDate { get; set; }

        public DateTime? StartDate { get; set; }

        public StudyMode StudyMode { get; set; }

        public CourseDetailResponseVenue Venue { get; set; }
    }
}