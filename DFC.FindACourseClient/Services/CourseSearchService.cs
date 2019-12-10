using AutoMapper;
using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Extensions;
using DFC.FindACourseClient.Models.APIRequests;
using DFC.FindACourseClient.Models.ExternalInterfaceModels;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.FindACourseClient.Services
{
    public class CourseSearchService : ICourseSearchService
    {
        private readonly IFindACourseClient findACourseClient;
        private readonly IAuditService auditService;
        private readonly IMapper mapper;

        public CourseSearchService(IFindACourseClient findACourseClient, IAuditService auditService, IMapper mapper)
        {
            this.findACourseClient = findACourseClient;
            this.auditService = auditService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(string jobProfileKeywords)
        {
            if (string.IsNullOrWhiteSpace(jobProfileKeywords))
            {
                return await Task.FromResult<IEnumerable<Course>>(null);
            }

            var request = BuildCourseListRequest(jobProfileKeywords);

            //if the the call to the courses API fails for anyreason we should log and continue as if there are no courses available.
            try
            {
                var apiResult = await findACourseClient.CourseSearchAsync(request);
                var requestSortBy = (SortType)Enum.Parse(typeof(SortType), request.SortBy.ToString());

                var response = new CourseSearchResult
                {
                    ResultProperties =
                    {
                        TotalPages = GetTotalPages((apiResult?.Total).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                        TotalResultCount = (apiResult?.Total).GetValueOrDefault(),
                        Page = GetCurrentPageNumber((apiResult?.Start).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                        OrderedBy = requestSortBy.GetCourseSearchOrderBy(),
                    },
                    Courses = mapper.Map<List<Course>>(apiResult?.Results),
                    //apiResult?.ConvertToSearchCourse(),
                };

                return response.Courses.SelectCoursesForJobProfile();
            }
            catch (Exception ex)
            {
                await auditService.CreateAudit(request, ex);
                return Enumerable.Empty<Course>();
            }
        }

        public async Task<CourseSearchResult> SearchCoursesAsync(CourseSearchProperties courseSearchProperties)
        {
            if (string.IsNullOrWhiteSpace(courseSearchProperties.Filters.SearchTerm))
            {
                return await Task.FromResult<CourseSearchResult>(null);
            }

            var request = BuildCourseSearchRequest(courseSearchProperties);
            var apiResult = await findACourseClient.CourseSearchAsync(request);

            return new CourseSearchResult
            {
                ResultProperties =
                {
                    TotalPages = GetTotalPages((apiResult?.Total).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                    TotalResultCount = (apiResult?.Total).GetValueOrDefault(),
                    Page = GetCurrentPageNumber((apiResult?.Start).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                    OrderedBy = courseSearchProperties.OrderedBy,
                },
                Courses = mapper.Map<List<Course>>(apiResult?.Results), //apiResult?.ConvertToSearchCourse(),
            };
        }

        public async Task<CourseDetails> GetCourseDetailsAsync(string courseId, string oppurtunityId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
            {
                return await Task.FromResult<CourseDetails>(null);
            }

            var request = BuildCourseGetRequest(courseId, oppurtunityId);
            var apiResult = await findACourseClient.CourseGetAsync(request);

            return mapper.Map<CourseDetails>(apiResult);
        }

        private static int GetTotalPages(int totalResults, int pageSize)
        {
            return (int)Math.Ceiling((decimal)totalResults / pageSize);
        }

        private static int GetCurrentPageNumber(int startingItem, int pageSize)
        {
            return (int)Math.Ceiling((decimal)startingItem / pageSize);
        }

        private static CourseSearchRequest BuildCourseListRequest(string request)
        {
            return new CourseSearchRequest
            {
                SubjectKeyword = request,
                StartDateFrom = DateTime.Now.ToString("yyyy-MM-dd"),
                DeliveryModes = new List<CourseType> { CourseType.All }.MapToDeliveryModes(),
                Limit = 20,
                Start = 1,
                SortBy = (int)SortType.A,
            };
        }

        private static CourseGetRequest BuildCourseGetRequest(string courseId, string courseRunId)
        {
            Guid.TryParse(courseId, out var courseGuid);
            Guid.TryParse(courseRunId, out var courseRunGuid);

            return new CourseGetRequest { CourseId = courseGuid, RunId = courseRunGuid };
        }

        private static CourseSearchRequest BuildCourseSearchRequest(CourseSearchProperties input)
        {
            return new CourseSearchRequest
            {
                Distance = input.Filters.DistanceSpecified ? input.Filters.Distance : default(float),
                Start = input.Count * (input.Page - 1),
                Limit = input.Count,
                DeliveryModes = input.Filters.CourseTypes?.MapToDeliveryModes(),
                StudyModes = input.Filters.CourseHours?.MapToStudyModes(),
                DfE1619Funded = input.Filters.Only1619Courses ? "Y" : null,
                Town = input.Filters?.Location,
                Postcode = input.Filters?.Location,
                SortBy = input.OrderedBy.GetSortType(),
                StartDateFrom = input.Filters.StartDate.GetEarliestStartDate(input.Filters.StartDateFrom),
                //StartDateTo = DateTime.UtcNow.ToShortDateString(),
                SubjectKeyword = input.Filters.SearchTerm,
                //QualificationLevels =
                //AttendancePatterns = input.Filters.
            };
        }
    }
}