﻿using DFC.FindACourseClient;
using System;
using System.Collections.Generic;
using Comp = DFC.FindACourseClient.Models.CompositeInterfaceModels.Enums;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseSearchFilters
    {
        public CourseSearchFilters()
        {
            this.CourseType = new List<Comp.CourseType>();
            this.CourseHours = new List<CourseHours>();
            this.LearningMethod = new List<LearningMethod>();
            this.CourseStudyTime = new List<AttendancePattern>();
            QualificationLevels = new List<string>();
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

        public List<CourseHours> CourseHours { get; set; }

        public IList<LearningMethod> LearningMethod { get; set; }

        public IList<Comp.CourseType> CourseType { get; set; }

        public IList<AttendancePattern> CourseStudyTime { get; set; }

        public List<string> QualificationLevels { get; set; }

        public bool IsValidStartDateFrom =>
            StartDate == StartDate.SelectDateFrom && !StartDateFrom.Equals(DateTime.MinValue);

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string CampaignCode { get; set; }
    }
}