using System;

namespace DFC.FindACourseClient
{
    public class CourseGetRequest
    {
        public Guid CourseId { get; set; }

        public Guid RunId { get; set; }
    }
}