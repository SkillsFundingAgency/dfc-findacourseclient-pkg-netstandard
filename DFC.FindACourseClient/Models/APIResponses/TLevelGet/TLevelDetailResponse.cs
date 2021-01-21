using System;
using System.Collections.Generic;

namespace DFC.FindACourseClient
{
    public class TLevelDetailResponse
    {
        public CourseOfferingType CourseOfferingType { get; set; }

        public Guid TLevelId { get; set; }

        public TLevelQualification Qualification { get; set; }

        public TLevelProvider Provider { get; set; }

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

        public DeliveryMode DeliveryMode { get; set; }

        public AttendancePattern AttendancePattern { get; set; }

        public StudyMode StudyMode { get; set; }

        public DurationUnit DurationUnit { get; set; }

        public int? DurationValue { get; set; }

        public decimal? Cost { get; set; }
    }
}
