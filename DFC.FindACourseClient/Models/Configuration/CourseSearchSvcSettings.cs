using System;

namespace DFC.FindACourseClient.Models.Configuration
{
    public class CourseSearchSvcSettings
    {
        public Uri CourseSearchUrl { get; set; }

        public Uri ServiceEndpoint { get; set; }

        public string ApiKey { get; set; }

        public string AttendanceModes { get; set; }

        public string SearchPageSize { get; set; }

        public int RequestTimeOutSeconds { get; set; }

        public int MaxReceivedMessageSize { get; set; }

        public int MaxBufferSize { get; set; }
    }
}
