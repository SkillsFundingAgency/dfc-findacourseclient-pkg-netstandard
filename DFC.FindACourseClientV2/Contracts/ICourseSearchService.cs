using DFC.FindACourseClientV2.Models.ExternalInterfaceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.FindACourseClientV2.Contracts
{
    public interface ICourseSearchService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string jobProfileKeywords);

        Task<CourseSearchResult> SearchCoursesAsync(CourseSearchProperties courseSearchProperties);

        Task<CourseDetails> GetCourseDetailsAsync(string courseId, string oppurtunityId);
    }
}