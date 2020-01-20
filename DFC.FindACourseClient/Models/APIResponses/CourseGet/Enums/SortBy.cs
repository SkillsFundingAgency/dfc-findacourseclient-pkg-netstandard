using System.ComponentModel;

namespace DFC.FindACourseClient
{
    public enum SortBy
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Relevance")]
        Relevance = 1,

        [Description("StartDateDescending")]
        StartDateDescending = 2,

        [Description("StartDateAscending")]
        StartDateAscending = 3,

        [Description("Distance")]
        Distance = 4,
    }
}
