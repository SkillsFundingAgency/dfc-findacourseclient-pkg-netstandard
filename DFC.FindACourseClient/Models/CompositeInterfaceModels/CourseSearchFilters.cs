using DFC.FindACourseClient;
using System;
using System.Collections.Generic;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseSearchFilters
    {
        public CourseSearchFilters()
        {
            this.CourseHours = new List<CourseHours>();
            this.CourseType = new List<CourseType>();
            this.CourseStudyTime = new List<AttendancePattern>();
        }

        public string SearchTerm { get; set; }

        public DateTime StartDateFrom { get; set; }

        public string Provider { get; set; }

        public string PostCode { get; set; }

        public float Distance { get; set; } = 10f;

        public string Town { get; set; }

        public bool DistanceSpecified { get; set; }

        public StartDate StartDate { get; set; } = StartDate.Anytime;

        public List<CourseHours> CourseHours { get; set; }

        public IList<CourseType> CourseType { get; set; }

        public IList<AttendancePattern> CourseStudyTime { get; set; }

        public bool IsValidStartDateFrom =>
            StartDate == StartDate.SelectDateFrom && !StartDateFrom.Equals(DateTime.MinValue);
        
    }
}