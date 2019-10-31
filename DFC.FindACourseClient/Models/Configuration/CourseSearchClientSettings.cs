using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClient.Models.Configuration
{
    public class CourseSearchClientSettings
    {
        public CourseSearchSvcSettings CourseSearchSvcSettings { get; set; }

        public CourseSearchAuditCosmosDbSettings CourseSearchAuditCosmosDbSettings { get; set; }
    }
}
