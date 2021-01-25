using System.ComponentModel;

namespace DFC.FindACourseClient
{
    public enum StudyMode
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Full-time")]
        FullTime = 1,

        [Description("Part-time")]
        PartTime = 2,

        [Description("Flexible")]
        Flexible = 3,
    }
}