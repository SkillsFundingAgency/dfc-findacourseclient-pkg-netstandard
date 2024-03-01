using System;

namespace DFC.FindACourseClient
{
    public class Result
    {
        public double SearchScore { get; set; }

        public string Distance { get; set; }

        public VenueLocation VenueLocation { get; set; }

        public Guid CourseId { get; set; }

        public Guid TLevelId { get; set; }

        public Guid TLevelLocationId { get; set; }

        public CourseOfferingType OfferingType { get; set; }

        public Guid CourseRunId { get; set; }

        public string QualificationCourseTitle { get; set; }

        public string LearnAimRef { get; set; }

        public string NotionalNVQLevelv2 { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string VenueName { get; set; }

        public string VenueAddress { get; set; }

        public string VenueAttendancePattern { get; set; }

        public string VenueAttendancePatternDescription { get; set; }

        public string ProviderName { get; set; }

        public string Region { get; set; }

        public string VenueStudyMode { get; set; }

        public string VenueStudyModeDescription { get; set; }

        public string DeliveryMode { get; set; }

        public string DeliveryModeDescription { get; set; }

        public DateTime? StartDate { get; set; }

        public string VenueTown { get; set; }

        public string Cost { get; set; }

        public string CostDescription { get; set; }

        public string CourseText { get; set; }

        public string UKprn { get; set; }

        public string CourseDescription { get; set; }

        public string CourseName { get; set; }

        public string QualificationLevel { get; set; }

        public DurationUnit DurationUnit { get; set; }

        public int? DurationValue { get; set; }

        public bool? FlexibleStartDate { get; set; }

        public int? SectorId { get; set; }

        public string SectorDescription { get; set; }

        public string CourseType { get; set; }

        public string CourseTypeDescription { get; set; }

        public EducationLevel? EducationLevel { get; set; }

        public string EducationLevelDescription { get; set; }

        public string AwardingBody { get; set; }
    }
}