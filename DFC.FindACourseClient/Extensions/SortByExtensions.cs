using System;
using System.Collections.Generic;
using System.Text;

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
                    result = SortBy.StartDateDescending;
                    break;
                case CourseSearchOrderBy.Distance:
                    result = SortBy.Distance;
                    break;
                default:
                    break;
            }

            return (int)result;
        }
    }
}
