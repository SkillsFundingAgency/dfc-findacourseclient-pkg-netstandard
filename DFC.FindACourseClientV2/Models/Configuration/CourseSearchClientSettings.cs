using System.Diagnostics.CodeAnalysis;

namespace DFC.FindACourseClientV2.Models.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CourseSearchClientSettings
    {
        public CourseSearchSvcSettings CourseSearchSvcSettings { get; set; }

        public CourseSearchAuditCosmosDbSettings CourseSearchAuditCosmosDbSettings { get; set; }
    }
}