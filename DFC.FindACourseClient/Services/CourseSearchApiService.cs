using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    public class CourseSearchApiService : ICourseSearchApiService
    {
        private readonly IFindACourseClient findACourseClient;
        private readonly IAuditService auditService;
        private readonly IMapper mapper;

        public CourseSearchApiService(IFindACourseClient findACourseClient, IAuditService auditService, IMapper mapper)
        {
            this.findACourseClient = findACourseClient;
            this.auditService = auditService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(string jobProfileKeywords, bool shouldThrowException = false)
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

                var response = new CourseSearchResult
                {
                    ResultProperties =
                    {
                        TotalPages = GetTotalPages((apiResult?.Total).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                        TotalResultCount = (apiResult?.Total).GetValueOrDefault(),
                        Page = GetCurrentPageNumber((apiResult?.Start).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                        OrderedBy = (CourseSearchOrderBy)Enum.Parse(typeof(CourseSearchOrderBy), request.SortBy.ToString()),
                    },
                    Courses = mapper.Map<List<Course>>(apiResult?.Results),
                };

                return response.Courses.SelectCoursesForJobProfile();
            }
            catch (Exception ex)
            {
                auditService.CreateAudit(request, ex);
                if (shouldThrowException)
                {
                    throw ex;
                }

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
            var apiResult = await findACourseClient.CourseSearchAsync(request).ConfigureAwait(false);

            return new CourseSearchResult
            {
                ResultProperties =
                {
                    TotalPages = apiResult?.Total == 0 ? 0 : GetTotalPages((apiResult?.Total).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                    TotalResultCount = (apiResult?.Total).GetValueOrDefault(),
                    Page = apiResult?.Start == 0 ? 0 : GetCurrentPageNumber((apiResult?.Start).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                    OrderedBy = courseSearchProperties.OrderedBy,
                },
                Courses = mapper.Map<List<Course>>(apiResult?.Results),
                AttachedSectorIds = apiResult?.Facets?.SectorId?.Select(s => s.Value).ToList() ?? new List<string>(),
            };
        }

        public async Task<Comp.CourseSearchResult> SearchCoursesAsync(Comp.CourseSearchProperties courseSearchProperties)
        {
            if (courseSearchProperties.Filters.SearchTerm == null)
            {
                courseSearchProperties.Filters.SearchTerm = string.Empty;
            }

            var request = BuildCourseSearchRequest(courseSearchProperties);
            var apiResult = await findACourseClient.CourseSearchAsync(request).ConfigureAwait(false);

            return new Comp.CourseSearchResult
            {
                ResultProperties =
                {
                    TotalPages = apiResult?.Total == 0 ? 0 : GetTotalPages((apiResult?.Total).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                    TotalResultCount = (apiResult?.Total).GetValueOrDefault(),
                    Page = apiResult?.Start == 0 ? 0 : GetCurrentPageNumber((apiResult?.Start).GetValueOrDefault(), (apiResult?.Limit).GetValueOrDefault()),
                    OrderedBy = courseSearchProperties.OrderedBy,
                },
                Courses = mapper.Map<List<Comp.Course>>(apiResult?.Results),
                AttachedSectorIds = apiResult?.Facets?.SectorId?.Select(s => s.Value).ToList() ?? new List<string>(),
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

        public async Task<Comp.CourseDetails> GetCompositeCourseDetailsAsync(string courseId, string oppurtunityId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
            {
                return await Task.FromResult<Comp.CourseDetails>(null);
            }

            var request = BuildCourseGetRequest(courseId, oppurtunityId);
            var apiResult = await findACourseClient.CourseGetAsync(request);

            return mapper.Map<Comp.CourseDetails>(apiResult);
        }

        public async Task<Comp.TLevelDetails> GetTLevelDetailsAsync(string tLevelId)
        {
            if (string.IsNullOrWhiteSpace(tLevelId))
            {
                return await Task.FromResult<Comp.TLevelDetails>(null);
            }

            var apiResult = await findACourseClient.TLevelGetAsync(tLevelId);

            return mapper.Map<Comp.TLevelDetails>(apiResult);
        }

        public async Task<List<Sector>> GetSectorsAsync()
        {
            var apiResult = await findACourseClient.SectorsGetAsync();

            return apiResult;
        }

        private static int GetTotalPages(int totalResults, int pageSize)
        {
            return (int)Math.Ceiling((decimal)totalResults / pageSize);
        }

        private static int GetCurrentPageNumber(int startingItem, int pageSize)
        {
            return (int)Math.Ceiling((decimal)startingItem / pageSize) + 1;
        }

        private static CourseSearchRequest BuildCourseListRequest(string request)
        {
            return new CourseSearchRequest
            {
                SubjectKeyword = request,
                StartDateFrom = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc).ToString("o"),
                CourseTypes = CourseType.All.MapToCourseTypes(),
                DeliveryModes = LearningMethod.All.MapToDeliveryModes(),
                Limit = 20,
                Start = 0,
                SortBy = (int)CourseSearchOrderBy.Relevance,
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
                Distance = input.Filters.DistanceSpecified ? input.Filters.Distance : default,
                Start = input.Count * (input.Page - 1),
                Limit = input.Count,
                DeliveryModes = input.Filters.LearningMethod.MapToDeliveryModes(),
                StudyModes = input.Filters.CourseHours.MapToStudyModes(),
                Town = input.Filters?.Town,
                Postcode = input.Filters?.PostCode,
                SortBy = input.OrderedBy.MapToSortBy(),
                StartDateFrom = input.Filters.StartDate.GetEarliestStartDate(input.Filters.StartDateFrom),
                SubjectKeyword = input.Filters.SearchTerm,
                ProviderName = input.Filters?.Provider,
                CampaignCode = input.Filters?.CampaignCode,
                CourseTypes = input.Filters?.CourseType.MapToCourseTypes(),
                SectorIds = input.Filters?.SectorIds,
                EducationLevels = input.Filters.EducationLevel.MapToEducationLevels(),
            };
        }

        private static CourseSearchRequest BuildCourseSearchRequest(Comp.CourseSearchProperties input)
        {
            return new CourseSearchRequest
            {
                Distance = input.Filters.DistanceSpecified ? input.Filters.Distance : default,
                Start = input.Count * (input.Page - 1),
                Limit = input.Count,
                DeliveryModes = input.Filters.LearningMethod.MapToCompositeDeliveryModes(),
                StudyModes = input.Filters.CourseHours.MapToCompositeStudyModes(),
                AttendancePatterns = input.Filters.CourseStudyTime.MapToCompositeAttendancePattern(),
                QualificationLevels = input.Filters.QualificationLevels,
                Town = input.Filters?.Town,
                Postcode = input.Filters?.PostCode,
                SortBy = input.OrderedBy.MapToCompositeSortBy(),
                StartDateFrom = input.Filters.StartDate.GetEarliestCompositeStartDate(input.Filters.StartDateFrom),
                SubjectKeyword = input.Filters.SearchTerm,
                ProviderName = input.Filters?.Provider,
                StartDateTo = (input.Filters.StartDateTo < DateTime.Now) ? null : input.Filters.StartDateTo.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Longitude = input.Filters.Longitude,
                Latitude = input.Filters.Latitude,
                CampaignCode = input.Filters?.CampaignCode,
                CourseTypes = input.Filters.CourseType.MapToCompositeCourseTypes(),
                SectorIds = input.Filters?.SectorIds.ToList(),
                EducationLevels = input.Filters.EducationLevel.MapToCompositeEducationLevels(),
            };
        }
    }
}