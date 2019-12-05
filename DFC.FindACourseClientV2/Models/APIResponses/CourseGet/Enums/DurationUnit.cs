using System.ComponentModel;

namespace DFC.FindACourseClientV2.Models.APIResponses.CourseGet.Enums
{
    public enum DurationUnit
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Days")]
        Days = 1,

        [Description("Weeks")]
        Weeks = 2,

        [Description("Months")]
        Months = 3,

        [Description("Years")]
        Years = 4,

        [Description("Hours")]
        Hours = 5,
    }
}