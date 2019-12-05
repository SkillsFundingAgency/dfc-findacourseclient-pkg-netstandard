using DFC.FindACourseClientV2.Models.ExternalInterfaceModels;
using DFC.FindACourseClientV2.Models.ExternalInterfaceModels.Enums;

namespace DFC.FindACourseClientV2.Extensions
{
    public static class SortTypeExtensions
    {
        public static int GetSortType(this CourseSearchOrderBy courseSearchSortBy)
        {
            switch (courseSearchSortBy)
            {
                case CourseSearchOrderBy.Distance:
                    return (int)SortType.D;

                case CourseSearchOrderBy.StartDate:
                    return (int)SortType.S;

                default:
                case CourseSearchOrderBy.Relevance:
                    return (int)SortType.A;
            }
        }

        public static CourseSearchOrderBy GetCourseSearchOrderBy(this SortType sortType)
        {
            switch (sortType)
            {
                case SortType.D:
                    return CourseSearchOrderBy.Distance;

                case SortType.S:
                    return CourseSearchOrderBy.StartDate;

                default:
                case SortType.A:
                    return CourseSearchOrderBy.Relevance;
            }
        }
    }
}