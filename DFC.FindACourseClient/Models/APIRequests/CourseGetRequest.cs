using System;

namespace DFC.FindACourseClient.Models.APIRequests
{
    public class CourseGetRequest
    {
        public Guid CourseId { get; set; }

        public Guid RunId { get; set; }
    }
}