using DFC.FindACourseClientV2.Models;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DFC.FindACourseClientV2.Contracts
{
    public interface IFindACourseClient
    {
        Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest);

        Task<CourseDetailsResponse> CourseGetAsync(CourseGetRequest courseGetRequest);
    }
}
