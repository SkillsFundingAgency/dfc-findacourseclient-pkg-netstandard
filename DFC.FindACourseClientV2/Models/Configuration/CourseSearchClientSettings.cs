﻿namespace DFC.FindACourseClientV2.Models.Configuration
{
    public class CourseSearchClientSettings
    {
        public CourseSearchSvcSettings CourseSearchSvcSettings { get; set; }

        public CourseSearchAuditCosmosDbSettings CourseSearchAuditCosmosDbSettings { get; set; }
    }
}