using DFC.FindACourseClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.FindACourseClient.Contracts
{
    public interface ICourseSearchClient
    {
        Task<IEnumerable<CourseSumary>> GetCoursesAsync(string courseSearchKeywords);
    }
}
