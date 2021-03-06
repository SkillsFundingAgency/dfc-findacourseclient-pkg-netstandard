﻿using System;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    internal static class StartDateExtensions
    {
        internal static string GetEarliestStartDate(this StartDate startDate, DateTime earliestStartDate)
        {
            const string CourseApiDateFormat = "yyyy-MM-ddTHH:mm:ssZ";
            switch (startDate)
            {
                case StartDate.FromToday:
                    return DateTime.Now.ToString(CourseApiDateFormat);

                case StartDate.SelectDateFrom:
                    return CalculateEarliestStartDate(earliestStartDate).ToString(CourseApiDateFormat);

                case StartDate.Anytime:
                default:
                    return null;
            }
        }

        internal static string GetEarliestCompositeStartDate(this Comp.StartDate startDate, DateTime earliestStartDate)
        {
            const string CourseApiDateFormat = "yyyy-MM-ddTHH:mm:ssZ";
            switch (startDate)
            {
                case Comp.StartDate.FromToday:
                    return DateTime.Now.ToString(CourseApiDateFormat);

                case Comp.StartDate.SelectDateFrom:
                    return CalculateEarliestStartDate(earliestStartDate).ToString(CourseApiDateFormat);

                case Comp.StartDate.Anytime:
                default:
                    return null;
            }
        }

        private static DateTime CalculateEarliestStartDate(DateTime inputDate)
        {
            var earliestDate = DateTime.Now.AddYears(-1);

            return inputDate < earliestDate ? earliestDate : inputDate;
        }
    }
}