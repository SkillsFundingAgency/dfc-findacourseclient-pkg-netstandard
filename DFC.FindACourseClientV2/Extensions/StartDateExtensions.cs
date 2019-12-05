using DFC.FindACourseClientV2.Models.ExternalInterfaceModels;
using System;

namespace DFC.FindACourseClientV2.Extensions
{
    public static class StartDateExtensions
    {
        public static string GetEarliestStartDate(this StartDate startDate, DateTime earliestStartDate)
        {
            const string CourseApiDateFormat = "yyyy-MM-dd";
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

        private static DateTime CalculateEarliestStartDate(DateTime inputDate)
        {
            var earliestDate = DateTime.Now.AddYears(-1);

            return inputDate < earliestDate ? earliestDate : inputDate;
        }
    }
}