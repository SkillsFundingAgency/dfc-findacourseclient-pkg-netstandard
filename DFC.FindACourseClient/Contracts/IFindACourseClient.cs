using DFC.FindACourseClient.Models.APIRequests;
using DFC.FindACourseClient.Models.APIResponses.CourseGet;
using DFC.FindACourseClient.Models.APIResponses.CourseSearch;
using System.Threading.Tasks;

namespace DFC.FindACourseClient.Contracts
{
    public interface IFindACourseClient
    {
        Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest);

        Task<CourseRunDetailResponse> CourseGetAsync(CourseGetRequest courseGetRequest);
    }
}