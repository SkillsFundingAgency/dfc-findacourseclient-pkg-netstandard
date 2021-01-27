using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public class TLevelDetails
    {
        public Guid TLevelId { get; set; }

        public TLevelQualification Qualification { get; set; }

        public ProviderDetails ProviderDetails { get; set; }

        public string WhoFor { get; set; }

        public string EntryRequirements { get; set; }

        public string WhatYoullLearn { get; set; }

        public string HowYoullLearn { get; set; }

        public string HowYoullBeAssessed { get; set; }

        public string WhatYouCanDoNext { get; set; }

        public string YourReference { get; set; }

        public string Website { get; set; }

        public DateTime? StartDate { get; set; }

        public List<TLevelLocation> Locations { get; set; }

        public string DeliveryMode { get; set; }

        public string AttendancePattern { get; set; }

        public string StudyMode { get; set; }

        public string Duration { get; set; }

        public string Cost { get; set; }

        public string CostDescription { get; set; }
    }
}
