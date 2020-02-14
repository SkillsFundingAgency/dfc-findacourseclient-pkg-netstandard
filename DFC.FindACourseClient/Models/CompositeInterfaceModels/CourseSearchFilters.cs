using System;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseSearchFilters
    {
        public string SearchTerm { get; set; }

        public DateTime StartDateFrom { get; set; }

        public string Provider { get; set; }

        public string PostCode { get; set; }

        public float Distance { get; set; } = 10f;

        public string Town { get; set; }

        public bool DistanceSpecified { get; set; }

        public StartDate StartDate { get; set; } = StartDate.Anytime;

        public CourseHours CourseHours { get; set; }

        public CourseType CourseType { get; set; }

        public bool IsValidStartDateFrom =>
            StartDate == StartDate.SelectDateFrom && !StartDateFrom.Equals(DateTime.MinValue);
    }
}