using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.FindACourseClientV2.Models
{
    public class CourseSumary
    {
        public string Title { get; set; }

        public string CourseId { get; set; }

        public string Provider { get; set; }

        public DateTime StartDate { get; set; }
    }
}
