using DFC.FindACourseClient.Models.Configuration;
using System;
using System.Net.Http;

namespace DFC.FindACourseClient.Services
{
    public class HttpClientService : IHttpClientService, IDisposable
    {
        private const string ApimSubscriptionKey = "Ocp-Apim-Subscription-Key";
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private HttpClient httpClient;

        public HttpClientService(CourseSearchClientSettings courseSearchClientSettings)
        {
            this.courseSearchClientSettings = courseSearchClientSettings;
        }

        private bool IsDisposing { get; set; }

        public HttpClient GetClient()
        {
            if (httpClient is null)
            {
                httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds),
                };

                httpClient.DefaultRequestHeaders.Add(ApimSubscriptionKey, courseSearchClientSettings.CourseSearchSvcSettings.ApiKey);
            }

            return httpClient;
        }

        public void Dispose()
        {
            if (!IsDisposing)
            {
                IsDisposing = true;
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
        }
    }
}
