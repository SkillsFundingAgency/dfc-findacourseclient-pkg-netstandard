using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.FindACourseClient.Models.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CourseSearchSvcSettings
    {
        public Uri ServiceEndpoint { get; set; }

        public string ApiKey { get; set; }

        public string SearchPageSize { get; set; }

        public int RequestTimeOutSeconds { get; set; }

        public int TransintErrorsNumberOfRetries { get; set; }
    }
}