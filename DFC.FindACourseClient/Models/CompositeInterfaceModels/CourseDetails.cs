﻿using System.Collections.Generic;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class CourseDetails : Course
    {
        public Venue VenueDetails { get; set; }

        public ProviderDetails ProviderDetails { get; set; }

        public IList<AlternativeCourseRun> AlternativeCourseRuns { get; set; }

        public string EntryRequirements { get; set; }

        public string QualificationName { get; set; }

        public string AssessmentMethod { get; set; }

        public string EquipmentRequired { get; set; }

        public string AwardingOrganisation { get; set; }

        public string SubjectCategory { get; set; }

        public string CourseWebpageLink { get; set; }

        public string AdditionalPrice { get; set; }

        public string SupportingFacilities { get; set; }

        public string LanguageOfInstruction { get; set; }

        public List<SubRegion> SubRegions { get; set; }

        public string NextSteps { get; set; }

        public string WhatYoullLearn { get; set; }

        public string HowYoullLearn { get; set; }
    }
}