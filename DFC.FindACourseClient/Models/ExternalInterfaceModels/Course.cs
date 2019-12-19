﻿using System;

namespace DFC.FindACourseClient.Models.ExternalInterfaceModels
{
    public class Course
    {
        public string Title { get; set; }

        public string CourseId { get; set; }

        public string ProviderName { get; set; }

        public DateTime StartDate { get; set; }

        public string Location { get; set; }

        public LocationDetails LocationDetails { get; set; }

        public string Duration { get; set; }

        public string QualificationLevel { get; set; }

        public string AttendanceMode { get; set; }

        public string AttendancePattern { get; set; }

        public string StudyMode { get; set; }

        public string StartDateLabel { get; set; }

        public bool AdvancedLearnerLoansOffered { get; set; }
    }
}