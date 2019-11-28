using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Contracts.CosmosDb;
using DFC.FindACourseClientV2.Models;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.APIResponses;
using DFC.FindACourseClientV2.Models.Configuration;
using DFC.FindACourseClientV2.Models.CosmosDb;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DFC.FindACourseClientV2
{
    public class FindACourseClient : IFindACourseClient
    {
        private readonly ILogger<FindACourseClient> logger;
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private readonly ICosmosRepository<APIAuditRecordCourse> auditRepository;
        private readonly HttpClient httpClient;
        private readonly Guid correlationId;

        public FindACourseClient(HttpClient httpClient, CourseSearchClientSettings courseSearchClientSettings, ILogger<FindACourseClient> logger = null, ICosmosRepository<APIAuditRecordCourse> auditRepository = null)
        {
            this.logger = logger;
            this.auditRepository = auditRepository;
            this.courseSearchClientSettings = courseSearchClientSettings ?? throw new ArgumentException(nameof(courseSearchClientSettings));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //this.httpClient.Timeout = TimeSpan.FromSeconds(courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds);
            this.correlationId = Guid.NewGuid();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", courseSearchClientSettings.CourseSearchSvcSettings.ApiKey);
        }

        public async Task<CourseDetailsResponse> CourseGetAsync(CourseGetRequest courseGetRequest)
        {
            var response = await httpClient.PostAsync($"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}courseget", courseGetRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            string responseContent = await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
            var auditRecord = new APIAuditRecordCourse() { DocumentId = Guid.NewGuid(), CorrelationId = correlationId, Request = courseGetRequest, Response = responseContent };
            await auditRepository.UpsertAsync(auditRecord).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Error status {response.StatusCode},  Getting API data for request :'{courseGetRequest}' \nResponse : {responseContent}");

                //this will throw an exception as is not a success code
                response.EnsureSuccessStatusCode();
            }

            return JsonConvert.DeserializeObject<CourseDetailsResponse>(responseContent);
        }

        public async Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest)
        {
            var response = await httpClient.PostAsync($"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}coursesearch", courseSearchRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            string responseContent = await (response.Content?.ReadAsStringAsync()).ConfigureAwait(false);
            var auditRecord = new APIAuditRecordCourse() { DocumentId = Guid.NewGuid(), CorrelationId = correlationId, Request = courseSearchRequest, Response = responseContent };
            await auditRepository.UpsertAsync(auditRecord).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Error status {response.StatusCode},  Getting API data for request :'{courseSearchRequest}' \nResponse : {responseContent}");

                //this will throw an exception as is not a success code
                response.EnsureSuccessStatusCode();
            }

            return JsonConvert.DeserializeObject<CourseSearchResponse>(responseContent);
        }
    }
}
