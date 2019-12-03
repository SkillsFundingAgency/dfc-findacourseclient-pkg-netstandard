using System;

namespace DFC.FindACourseClientV2.Models.APIRequests
{
    public class CourseGetRequest
    {
        public Guid CourseId { get; set; }

        public Guid RunId { get; set; }
    }
}