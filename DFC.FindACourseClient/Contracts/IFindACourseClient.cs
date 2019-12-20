using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    public interface IFindACourseClient
    {
        Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest);

        Task<CourseRunDetailResponse> CourseGetAsync(CourseGetRequest courseGetRequest);
    }
}