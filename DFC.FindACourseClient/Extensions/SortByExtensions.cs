using System;
using System.Collections.Generic;
using System.Text;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    internal static class SortByExtensions
    {
        internal static int MapToSortBy(this CourseSearchOrderBy orderBy)
        {
            var result = SortBy.Undefined;

            switch (orderBy)
            {
                case CourseSearchOrderBy.Relevance:
                    result = SortBy.Relevance;
                    break;
                case CourseSearchOrderBy.StartDate:
                    result = SortBy.AscendingStartDate;
                    break;
                case CourseSearchOrderBy.Distance:
                    result = SortBy.Distance;
                    break;
                default:
                    break;
            }

            return (int)result;
        }

        internal static int MapToCompositeSortBy(this Comp.CourseSearchOrderBy orderBy)
        {
            var result = SortBy.Undefined;

            switch (orderBy)
            {
                case Comp.CourseSearchOrderBy.Relevance:
                    result = SortBy.Relevance;
                    break;
                case Comp.CourseSearchOrderBy.StartDate:
                    result = SortBy.AscendingStartDate;
                    break;
                case Comp.CourseSearchOrderBy.Distance:
                    result = SortBy.Distance;
                    break;
                default:
                    break;
            }

            return (int)result;
        }
    }
}
