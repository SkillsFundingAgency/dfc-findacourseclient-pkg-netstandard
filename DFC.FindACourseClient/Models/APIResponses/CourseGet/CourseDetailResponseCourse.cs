using System;

namespace DFC.FindACourseClient
{
    public class CourseDetailResponseCourse
    {
        public string AwardOrgCode { get; set; }

        public string CourseDescription { get; set; }

        public Guid CourseId { get; set; }

        public string LearnAimRef { get; set; }

        public string QualificationLevel { get; set; }

        public string WhatYoullLearn { get; set; }

        public string WhatYoullNeed { get; set; }

        public string WhereNext { get; set; }

        public string EntryRequirements { get; set; }

        public string HowYoullBeAssessed { get; set; }

        public string HowYoullLearn { get; set; }

        public int? SectorId { get; set; }

        public string SectorDescription { get; set; }

        public CourseType? CourseType { get; set; }

        public string CourseTypeDescription { get; set; }

        public EducationLevel? EducationLevel { get; set; }

        public string EducationLevelDescription { get; set; }

        public string AwardingBody { get; set; }
    }
}