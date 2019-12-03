using System;

namespace DFC.FindACourseClientV2.Models.Configuration
{
    public class CourseSearchSvcSettings
    {
        public Uri ServiceEndpoint { get; set; }

        public string ApiKey { get; set; }

        public string AttendanceModes { get; set; }

        public string SearchPageSize { get; set; }

        public int RequestTimeOutSeconds { get; set; }

        public int TransintErrorsNumberOfRetries { get; set; }
    }
}