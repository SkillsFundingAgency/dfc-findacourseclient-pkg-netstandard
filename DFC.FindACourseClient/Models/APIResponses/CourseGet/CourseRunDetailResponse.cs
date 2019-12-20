using System;
using System.Collections.Generic;

namespace DFC.FindACourseClient
{
    public class CourseRunDetailResponse
    {
        public CourseDetailResponseProvider Provider { get; set; }

        public CourseDetailResponseCourse Course { get; set; }

        public CourseDetailResponseVenue Venue { get; set; }

        public CourseDetailResponseQualification Qualification { get; set; }

        public IEnumerable<CourseDetailResponseAlternativeCourseRun> AlternativeCourseRuns { get; set; }

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
    }
}