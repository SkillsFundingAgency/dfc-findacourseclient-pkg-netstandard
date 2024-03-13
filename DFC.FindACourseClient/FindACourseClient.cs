using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    public class FindACourseClient : IFindACourseClient
    {
        private readonly ILogger<IFindACourseClient> logger;
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private readonly Guid correlationId;
        private readonly IAuditService auditService;
        private readonly HttpClient httpClient;

        public FindACourseClient(HttpClient httpClient, CourseSearchClientSettings courseSearchClientSettings, IAuditService auditService, ILogger<IFindACourseClient> logger = null)
        {
            correlationId = Guid.NewGuid();
            this.logger = logger;
            this.auditService = auditService;
            this.courseSearchClientSettings = courseSearchClientSettings;
            this.httpClient = httpClient;
        }

        public async Task<CourseRunDetailResponse> CourseGetAsync(CourseGetRequest courseGetRequest)
        {
            var responseContent = string.Empty;
            try
            {
                var url = $"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}courserundetail?CourseId={courseGetRequest.CourseId}&CourseRunId={courseGetRequest.RunId}";
                logger.LogDebug($"Get course called using url : {url}");

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

                logger.LogDebug($"Received response {response?.StatusCode} for url : {url}");
                if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
                {
                    logger?.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseGetRequest}' \nResponse : {responseContent}");
                    response?.EnsureSuccessStatusCode();
                }

                return JsonConvert.DeserializeObject<CourseRunDetailResponse>(responseContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Exception getting API data for request :'{courseGetRequest}'");
                return null;
            }
            finally
            {
                auditService.CreateAudit(courseGetRequest, responseContent, correlationId);
            }
        }

        public async Task<CourseSearchResponse> CourseSearchAsync(CourseSearchRequest courseSearchRequest)
        {
            var responseContent = string.Empty;
            try
            {
                var url = $"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}coursesearch";
                logger.LogDebug($"Search for courses POST : {url}, using properties: {JsonConvert.SerializeObject(courseSearchRequest)}");

                var response = await httpClient.PostAsync(url, courseSearchRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

                logger.LogDebug($"Received response {response?.StatusCode} for url : {url}, using properties: {JsonConvert.SerializeObject(courseSearchRequest)}");
                if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
                {
                    logger?.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseSearchRequest}' \nResponse : {responseContent}");

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return new CourseSearchResponse() { Total = 0, Limit = 0, Results = Enumerable.Empty<Result>() };
                    }

                    response?.EnsureSuccessStatusCode();
                }

                return JsonConvert.DeserializeObject<CourseSearchResponse>(responseContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Exception getting API data for request :'{courseSearchRequest}'");
                return null;
            }
            finally
            {
                auditService.CreateAudit(courseSearchRequest, responseContent, correlationId);
            }
        }

        public async Task<CourseSearchResponse> CourseSearchWithEnumListsAsync(CourseSearchRequest courseSearchRequest)
        {
            var responseContent = string.Empty;
            try
            {
                var url = $"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}coursesearch";
                logger.LogDebug($"Search for courses POST : {url}, using properties: {JsonConvert.SerializeObject(courseSearchRequest)}");

                var response = await httpClient.PostAsync(url, courseSearchRequest, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

                logger.LogDebug($"Received response {response?.StatusCode} for url : {url}, using properties: {JsonConvert.SerializeObject(courseSearchRequest)}");
                if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
                {
                    logger?.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{courseSearchRequest}' \nResponse : {responseContent}");

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return new CourseSearchResponse() { Total = 0, Limit = 0, Results = Enumerable.Empty<Result>() };
                    }

                    response?.EnsureSuccessStatusCode();
                }

                return JsonConvert.DeserializeObject<CourseSearchResponse>(responseContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Exception getting API data for request :'{courseSearchRequest}'");
                return null;
            }
            finally
            {
                auditService.CreateAudit(courseSearchRequest, responseContent, correlationId);
            }
        }

        public async Task<TLevelDetailResponse> TLevelGetAsync(string tLevelId)
        {
            var responseContent = string.Empty;
            try
            {
                var url = $"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}tleveldetail?tLevelId={tLevelId}";
                logger.LogDebug($"Getting TLevel  : {url}");

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

                logger.LogDebug($"Received response {response?.StatusCode} for url : {url}");
                if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
                {
                    logger?.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{url}' \nResponse : {responseContent}");
                    response?.EnsureSuccessStatusCode();
                }

                return JsonConvert.DeserializeObject<TLevelDetailResponse>(responseContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Exception getting API data for request :'{tLevelId}'");
                return null;
            }
            finally
            {
                auditService.CreateAudit(tLevelId, responseContent, correlationId);
            }
        }

        public async Task<List<Sector>> SectorsGetAsync()
        {
            var responseContent = string.Empty;
            try
            {
                var url = $"{courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint}sectors";
                logger.LogDebug($"Getting Sectors  : {url}");

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                responseContent = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

                logger.LogDebug($"Received response {response?.StatusCode} for url : {url}");
                if (!(response?.IsSuccessStatusCode).GetValueOrDefault())
                {
                    logger?.LogError($"Error status {response?.StatusCode},  Getting API data for request :'{url}' \nResponse : {responseContent}");
                    response?.EnsureSuccessStatusCode();
                }

                return JsonConvert.DeserializeObject<List<Sector>>(responseContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Exception getting API data for Sectors");
                return null;
            }
            finally
            {
                auditService.CreateAudit(null, responseContent, correlationId);
            }
        }
    }
}