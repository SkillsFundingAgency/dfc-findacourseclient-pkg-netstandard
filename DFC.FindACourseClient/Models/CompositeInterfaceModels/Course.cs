using System;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class Course
    {
        public string Title { get; set; }

        public string CourseId { get; set; }

        public string RunId { get; set; }

        public string TLevelId { get; set; }

        public string TLevelLocationId { get; set; }

        public OfferingType OfferingType { get; set; }

        public string ProviderName { get; set; }

        public DateTime? StartDate { get; set; }

        public bool FlexibleStartDate { get; set; }

        public string Description { get; set; }

        public string Cost { get; set; }

        public string CostDescription { get; set; }

        public string Location { get; set; }

        public LocationDetails LocationDetails { get; set; }

        public string Duration { get; set; }

        public string QualificationLevel { get; set; }

        public string AttendanceMode { get; set; }

        public string AttendancePattern { get; set; }

        public string StudyMode { get; set; }

        public string StartDateLabel { get; set; }

        public string Region { get; set; }

        public bool National { get; set; }

        public bool HasCampaignCode { get; set; }

        public string Sector { get; set; }

        public string CourseType { get; set; }

        public string EducationLevel { get; set; }

        public string AwardingBody { get; set; }
    }
}