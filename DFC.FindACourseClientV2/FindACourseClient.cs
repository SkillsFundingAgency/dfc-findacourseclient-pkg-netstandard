using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Contracts.CosmosDb;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.APIResponses.CourseGet;
using DFC.FindACourseClientV2.Models.APIResponses.CourseSearch;
using DFC.FindACourseClientV2.Models.Configuration;
using DFC.FindACourseClientV2.Models.CosmosDb;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DFC.FindACourseClientV2
{
    public class FindACourseClient : IFindACourseClient
    {
        private readonly ILogger<FindACourseClient> logger;
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private readonly ICosmosRepository<ApiAuditRecordCourse> auditRepository;
        private readonly HttpClient httpClient;
        private readonly Guid correlationId;

        public FindACourseClient(HttpClient httpClient, CourseSearchClientSettings courseSearchClientSettings, ILogger<FindACourseClient> logger = null, ICosmosRepository<ApiAuditRecordCourse> auditRepository = null)
        {
            this.logger = logger;
            this.auditRepository = auditRepository;
            this.courseSearchClientSettings = courseSearchClientSettings ?? throw new ArgumentException(nameof(courseSearchClientSettings));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.Timeout = TimeSpan.FromSeconds(courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds);
            correlationId = Guid.NewGuid();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", courseSearchClientSettings.CourseSearchSvcSettings.ApiKey);
        }

        public async Task<CourseDetailsResponse> CourseGetAsync(CourseGetRequest courseGetRequest)
        {
            var response = await httpClient.PostAsync($"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}courseget", courseGetRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            var responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
            var auditRecord = new ApiAuditRecordCourse
            {
                DocumentId = Guid.NewGuid(),
                CorrelationId = correlationId,
                Request = courseGetRequest,
                Response = responseContent,
            };

            await auditRepository.UpsertAsync(auditRecord).ConfigureAwait(false);

            if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
            {
                logger.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseGetRequest}' \nResponse : {responseContent}");
                response?.EnsureSuccessStatusCode();
            }

            return JsonConvert.DeserializeObject<CourseDetailsResponse>(responseContent);
        }

        public async Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest)
        {
            var response = await httpClient.PostAsync($"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}coursesearch", courseSearchRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            var responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
            var auditRecord = new ApiAuditRecordCourse
            {
                DocumentId = Guid.NewGuid(),
                CorrelationId = correlationId,
                Request = courseSearchRequest,
                Response = responseContent,
            };

            await auditRepository.UpsertAsync(auditRecord).ConfigureAwait(false);

            if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
            {
                logger.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseSearchRequest}' \nResponse : {responseContent}");
                response?.EnsureSuccessStatusCode();
            }

            return JsonConvert.DeserializeObject<CourseSearchResponse>(responseContent);
        }
    }
}