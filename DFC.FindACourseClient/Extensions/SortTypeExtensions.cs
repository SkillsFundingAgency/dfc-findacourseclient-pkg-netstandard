using DFC.FindACourseClient.Models.ExternalInterfaceModels;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;

namespace DFC.FindACourseClient.Extensions
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