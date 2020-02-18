using DFC.FindACourseClient;
using System;
using System.Collections.Generic;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseSearchFilters
    {
        public CourseSearchFilters()
        {
            this.StartDate = new List<StartDate>();
            this.CourseHours = new List<CourseHours>();
            this.CourseType = new List<CourseType>();
        }

        public string SearchTerm { get; set; }

        public DateTime StartDateFrom { get; set; }

        public string Provider { get; set; }

        public string PostCode { get; set; }

        public float Distance { get; set; } = 10f;

        public string Town { get; set; }

        public bool DistanceSpecified { get; set; }

        public IList<StartDate> StartDate { get; set; } //= StartDate.Anytime;

        public IList<CourseHours> CourseHours { get; set; }

        public IList<CourseType> CourseType { get; set; }

        public IList<StudyMode> CourseStudyMode { get; set; }

        //public bool IsValidStartDateFrom =>
        //    StartDate == StartDateSelectDateFrom && !StartDateFrom.Equals(DateTime.MinValue);
        public bool IsValidStartDateFrom = false;
    }
}