using System.Collections.Generic;
using System.Threading.Tasks;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    public interface ICourseSearchApiService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string jobProfileKeywords);

        Task<CourseSearchResult> SearchCoursesAsync(CourseSearchProperties courseSearchProperties);

        Task<CourseDetails> GetCourseDetailsAsync(string courseId, string oppurtunityId);

        
        Task<Comp.CourseDetails> GetCompositeCourseDetailsAsync(string courseId, string oppurtunityId);

        Task<Comp.CourseSearchResult> SearchCoursesAsync(Comp.CourseSearchProperties courseSearchProperties);
    }
}