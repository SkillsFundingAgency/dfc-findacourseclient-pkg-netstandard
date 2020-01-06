using AutoMapper;

namespace DFC.FindACourseClient.AutoMapperProfiles
{
    public class DetailsResponseStartDateValueConverter : IValueConverter<CourseRunDetailResponse, string>
    {
        public string Convert(CourseRunDetailResponse sourceMember, ResolutionContext context)
        {
            return sourceMember.FlexibleStartDate ? "Flexible" : sourceMember.StartDate?.ToString("dd MMMM yyyy");
        }
    }
}