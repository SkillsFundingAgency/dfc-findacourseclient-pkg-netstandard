using AutoMapper;

namespace DFC.FindACourseClient.AutoMapperProfiles
{
    public class StartDateValueConverter : IValueConverter<Result, string>
    {
        public string Convert(Result f, ResolutionContext context)
        {
            return f.FlexibleStartDate.GetValueOrDefault() ? "Flexible" : f.StartDate?.ToString("D");
        }
    }
}