using System.ComponentModel;

namespace DFC.FindACourseClientV2.Models.APIResponses.CourseGet.Enums
{
    public enum StartDateType
    {
        [Description("Defined Start Date")]
        SpecifiedStartDate = 1,

        [Description("Select a flexible start date")]
        FlexibleStartDate = 2,
    }
}