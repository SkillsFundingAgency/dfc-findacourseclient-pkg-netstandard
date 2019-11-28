using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClientV2.Models.APIRequests
{
    public class CourseGetRequest
    {
      public Guid CourseId { get; set; }

      public Guid RunId { get; set; }
    }
}
