﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClientV2.Models.APIResponses
{
    public class Result
    {
        public double SearchScore { get; set; }

        public object Distance { get; set; }

        public VenueLocation VenueLocation { get; set; }

        public Guid CourseId { get; set; }

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

        public object Cost { get; set; }

        public string CostDescription { get; set; }

        public string CourseText { get; set; }

        public string UKprn { get; set; }

        public object CourseDescription { get; set; }

        public object CourseName { get; set; }
    }
}
