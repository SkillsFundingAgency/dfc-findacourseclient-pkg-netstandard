using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DFC.FindACourseClient
{
    public enum CourseOfferingType
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Course")]
        Course = 1,

        [Description("TLevel")]
        TLevel = 2,
    }
}
