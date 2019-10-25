using DFC.FindACourseClient.Models;
using DFC.FindACourseClient.Models.Configuration;
using System.Collections.Generic;

namespace DFC.FindACourseClient.Contracts
{
    public interface IMessageConverter
    {
        CourseListInput GetCourseListRequest(string keyword, CourseSearchSvcSettings courseSearchSvcSettings);

        IEnumerable<CourseSumary> ConvertToCourse(CourseListOutput apiResult);
    }
}
