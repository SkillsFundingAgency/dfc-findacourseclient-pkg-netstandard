using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.FindACourseClient
{
    [ExcludeFromCodeCoverage]
    public class CourseSearchSvcSettings
    {
        public Uri ServiceEndpoint { get; set; }

        public string ApiKey { get; set; }

        public string SearchPageSize { get; set; } = "20";

        public int RequestTimeOutSeconds { get; set; } = 10;

        public int TransintErrorsNumberOfRetries { get; set; } = 3;
    }
}