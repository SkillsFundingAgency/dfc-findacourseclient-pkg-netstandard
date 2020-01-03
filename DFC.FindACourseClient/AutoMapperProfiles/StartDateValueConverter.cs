﻿using AutoMapper;

namespace DFC.FindACourseClient.AutoMapperProfiles
{
    public class StartDateValueConverter : IValueConverter<Result, string>
    {
        public string Convert(Result sourceMember, ResolutionContext context)
        {
            return sourceMember.FlexibleStartDate.GetValueOrDefault() ? "Flexible" : sourceMember.StartDate?.ToString("dd MMMM yyyy");
        }
    }
}