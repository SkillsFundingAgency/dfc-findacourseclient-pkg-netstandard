using System.Diagnostics.CodeAnalysis;

namespace DFC.FindACourseClient.Models.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CourseSearchClientSettings
    {
        public CourseSearchSvcSettings CourseSearchSvcSettings { get; set; }

        public CourseSearchAuditCosmosDbSettings CourseSearchAuditCosmosDbSettings { get; set; }
    }
}