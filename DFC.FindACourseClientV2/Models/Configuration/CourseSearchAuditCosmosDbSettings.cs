using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.FindACourseClientV2.Models.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CourseSearchAuditCosmosDbSettings
    {
        public string AccessKey { get; set; }

        public Uri EndpointUrl { get; set; }

        public string DatabaseId { get; set; }

        public string CollectionId { get; set; }

        public string PartitionKey { get; set; }
    }
}