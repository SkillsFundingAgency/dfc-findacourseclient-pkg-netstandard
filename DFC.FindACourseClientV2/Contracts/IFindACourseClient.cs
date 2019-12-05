using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.APIResponses.CourseGet;
using DFC.FindACourseClientV2.Models.APIResponses.CourseSearch;
using System.Threading.Tasks;

namespace DFC.FindACourseClientV2.Contracts
{
    public interface IFindACourseClient
    {
        Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest);

        Task<CourseRunDetailResponse> CourseGetAsync(CourseGetRequest courseGetRequest);
    }
}