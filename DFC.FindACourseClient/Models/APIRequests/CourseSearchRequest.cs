using System.Collections.Generic;

namespace DFC.FindACourseClient
{
    public class CourseSearchRequest
    {
        public string SubjectKeyword { get; set; }

        public double Distance { get; set; }

        public string ProviderName { get; set; }

        public List<string> QualificationLevels { get; set; }

        public List<StudyMode> StudyModes { get; set; }

        public List<DeliveryMode> DeliveryModes { get; set; }

        public List<CourseType> CourseTypes { get; set; }

        public List<int> AttendancePatterns { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }

        public int SortBy { get; set; }

        public string StartDateFrom { get; set; }

        public string StartDateTo { get; set; }

        public int Limit { get; set; }

        public int Start { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string CampaignCode { get; set; }

        public List<int> SectorIds { get; set; }

        public List<EducationLevel> EducationLevels { get; set; }

        public bool ShouldSerializeDistance()
        {
            return (Longitude != null && Latitude != null) || !string.IsNullOrEmpty(Postcode);
        }

        public bool ShouldSerializeProviderName()
        {
            return !string.IsNullOrEmpty(ProviderName);
        }

        public bool ShouldSerializeQualificationLevels()
        {
            return QualificationLevels?.Count > 0;
        }

        public bool ShouldSerializeStudyModes()
        {
            return StudyModes?.Count > 0;
        }

        public bool ShouldSerializeDeliveryModes()
        {
            return DeliveryModes?.Count > 0;
        }

        public bool ShouldSerializeAttendancePatterns()
        {
            return AttendancePatterns?.Count > 0;
        }

        public bool ShouldSerializeTown()
        {
            return !string.IsNullOrEmpty(Town);
        }

        public bool ShouldSerializePostcode()
        {
            return !string.IsNullOrEmpty(Postcode);
        }

        public bool ShouldSerializeStartDateFrom()
        {
            return !string.IsNullOrEmpty(StartDateFrom);
        }

        public bool ShouldSerializeStartDateTo()
        {
            return !string.IsNullOrEmpty(StartDateTo);
        }

        public bool ShouldSerializeLatitude()
        {
            return Latitude != null;
        }

        public bool ShouldSerializeLongitude()
        {
            return Longitude != null;
        }

        public bool ShouldSerializeCampaignCode()
        {
            return !string.IsNullOrEmpty(CampaignCode);
        }
    }
}