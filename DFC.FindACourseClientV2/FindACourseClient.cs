using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.APIResponses.CourseGet;
using DFC.FindACourseClientV2.Models.APIResponses.CourseSearch;
using DFC.FindACourseClientV2.Models.Configuration;
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
        private readonly ILogger logger;
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private readonly Guid correlationId;
        private readonly IAuditService auditService;
        private readonly HttpClient httpClient;

        public FindACourseClient(HttpClient httpClient, CourseSearchClientSettings courseSearchClientSettings, IAuditService auditService, ILogger logger = null)
        {
            correlationId = Guid.NewGuid();
            this.logger = logger;
            this.auditService = auditService;
            this.courseSearchClientSettings = courseSearchClientSettings ?? throw new ArgumentException(nameof(courseSearchClientSettings));

            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.Timeout = TimeSpan.FromSeconds(courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds);
            this.httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", courseSearchClientSettings.CourseSearchSvcSettings.ApiKey);
        }

        public async Task<CourseRunDetailResponse> CourseGetAsync(CourseGetRequest courseGetRequest)
        {
            var url = $"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}courserundetail?CourseId={courseGetRequest.CourseId}&CourseRunId={courseGetRequest.RunId}";
            var response = await httpClient.GetAsync(url).ConfigureAwait(false);
            var responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

            await auditService.CreateAudit(courseGetRequest, responseContent, correlationId).ConfigureAwait(false);

            if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
            {
                logger.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseGetRequest}' \nResponse : {responseContent}");
                response?.EnsureSuccessStatusCode();
            }

            return JsonConvert.DeserializeObject<CourseRunDetailResponse>(responseContent);
        }

        public async Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest)
        {
            var response = await httpClient.PostAsync($"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}coursesearch", courseSearchRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            var responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

            await auditService.CreateAudit(courseSearchRequest, responseContent, correlationId).ConfigureAwait(false);

            if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
            {
                logger.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseSearchRequest}' \nResponse : {responseContent}");
                response?.EnsureSuccessStatusCode();
            }

            return JsonConvert.DeserializeObject<CourseSearchResponse>(responseContent);
        }
    }
}