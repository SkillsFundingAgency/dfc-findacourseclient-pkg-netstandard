using System;
using System.Collections.Generic;

namespace DFC.FindACourseClient
{
    public class CourseSearchFilters
    {
        public CourseSearchFilters()
        {
            EducationLevel = EducationLevel.All;
        }

        public string SearchTerm { get; set; }

        public DateTime StartDateFrom { get; set; }

        public DateTime StartDateTo { get; set; }

        public string Provider { get; set; }

        public string PostCode { get; set; }

        public float Distance { get; set; } = 10f;

        public string Town { get; set; }

        public bool DistanceSpecified { get; set; }

        public StartDate StartDate { get; set; } = StartDate.Anytime;

        public CourseHours CourseHours { get; set; }

        public LearningMethod LearningMethod { get; set; }

        public CourseType CourseType { get; set; }

        public List<int> SectorIds { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public string CampaignCode { get; set; }

        public bool IsValidStartDateFrom =>
            StartDate == StartDate.SelectDateFrom && !StartDateFrom.Equals(DateTime.MinValue);
    }
}