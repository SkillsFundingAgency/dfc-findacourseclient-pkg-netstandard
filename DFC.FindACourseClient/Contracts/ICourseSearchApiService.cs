using System.Collections.Generic;
using System.Threading.Tasks;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    public interface ICourseSearchApiService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string jobProfileKeywords, bool shouldThrowException = false);

        Task<CourseSearchResult> SearchCoursesAsync(CourseSearchProperties courseSearchProperties);

        Task<CourseDetails> GetCourseDetailsAsync(string courseId, string oppurtunityId);

        Task<Comp.CourseDetails> GetCompositeCourseDetailsAsync(string courseId, string oppurtunityId);

        Task<Comp.CourseSearchResult> SearchCoursesAsync(Comp.CourseSearchProperties courseSearchProperties);

        Task<Comp.TLevelDetails> GetTLevelDetailsAsync(string tLevelId);

        Task<List<Sector>> GetSectorsAsync();
    }
}