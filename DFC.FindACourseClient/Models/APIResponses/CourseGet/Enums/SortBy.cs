using System.ComponentModel;

namespace DFC.FindACourseClient
{
    public enum SortBy
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Relevance")]
        Relevance = 1,

        [Description("DescendingStartDate")]
        DescendingStartDate = 2,

        [Description("AscendingStartDate")]
        AscendingStartDate = 3,

        [Description("Distance")]
        Distance = 4,

    }
}
