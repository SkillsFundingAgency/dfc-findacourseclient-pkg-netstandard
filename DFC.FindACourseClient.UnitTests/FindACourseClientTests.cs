using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Models.APIRequests;
using DFC.FindACourseClient.Models.APIResponses.CourseGet;
using DFC.FindACourseClient.Models.APIResponses.CourseSearch;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.UnitTests.ClientHandlers;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests
{
    public class FindACourseClientTests
    {
        private readonly Guid courseRunId = Guid.NewGuid();
        private readonly Guid courseId = Guid.NewGuid();

        private readonly CourseSearchClientSettings defaultSettings;
        private readonly ILogger defaultLogger;

        public FindACourseClientTests()
        {
            defaultLogger = A.Fake<ILogger>();
            defaultSettings = new CourseSearchClientSettings
            {
                CourseSearchSvcSettings = new CourseSearchSvcSettings
                {
                    ServiceEndpoint = new Uri("http://someurl.com"),
                    ApiKey = "SomeAPIMSubscriptionKey",
                    RequestTimeOutSeconds = 10,
                },
            };
        }

        [Fact]
        public async Task CourseGetAsyncReturnsResponseObjectWhenApiCallIsSuccessful()
        {
            // Arrange
            var courseGetRequest = new CourseGetRequest { CourseId = courseId, RunId = courseRunId };
            var expectedResponse = new CourseRunDetailResponse
            {
                CourseRunId = courseRunId,
                CourseName = "CourseName",
                Course = new CourseDetailResponseCourse { CourseId = courseId },
            };

            var httpResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) };

            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            var auditService = A.Fake<IAuditService>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri("http://SomeDummyUrl") };
            var httpClientService = A.Fake<IHttpClientService>();
            A.CallTo(() => httpClientService.GetClient()).Returns(httpClient);

            var findACourseClient = new FindACourseClient(httpClientService, defaultSettings, auditService, defaultLogger);

            // Act
            var result = await findACourseClient.CourseGetAsync(courseGetRequest).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.CourseName, result.CourseName);
            Assert.Equal(expectedResponse.CourseRunId, result.CourseRunId);
            Assert.Equal(expectedResponse.Course.CourseId, result.Course.CourseId);

            httpResponse.Dispose();
            httpClient.Dispose();
            fakeHttpMessageHandler.Dispose();
        }

        [Fact]
        public async Task CourseGetAsyncReturnsHttpRequestExceptionWhenApiReturnsNotSuccessful()
        {
            // Arrange
            var courseGetRequest = new CourseGetRequest { CourseId = courseId, RunId = courseRunId };

            var httpResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent("{}") };

            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            var auditService = A.Fake<IAuditService>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri("http://SomeDummyUrl") };
            var httpClientService = A.Fake<IHttpClientService>();
            A.CallTo(() => httpClientService.GetClient()).Returns(httpClient);
            var findACourseClient = new FindACourseClient(httpClientService, defaultSettings, auditService, defaultLogger);

            // Act
            await Assert.ThrowsAsync<HttpRequestException>(async () => await findACourseClient.CourseGetAsync(courseGetRequest).ConfigureAwait(false)).ConfigureAwait(false);

            // Assert
            httpResponse.Dispose();
            httpClient.Dispose();
            fakeHttpMessageHandler.Dispose();
        }

        [Fact]
        public async Task CourseSearchAsyncReturnsResponseObjectWhenApiCallIsSuccessful()
        {
            // Arrange
            var courseSearchRequest = new CourseSearchRequest { SubjectKeyword = "Somekeyword" };
            var expectedResponse = new CourseSearchResponse
            {
                Results = new List<Result>
                {
                    new Result
                    {
                        CourseId = courseId,
                        CourseRunId = courseRunId,
                        CourseName = "CourseName",
                    },
                },
            };

            var httpResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) };

            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            var auditService = A.Fake<IAuditService>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri("http://SomeDummyUrl") };
            var httpClientService = A.Fake<IHttpClientService>();
            A.CallTo(() => httpClientService.GetClient()).Returns(httpClient);
            var findACourseClient = new FindACourseClient(httpClientService, defaultSettings, auditService, defaultLogger);

            // Act
            var result = await findACourseClient.CourseSearchAsync(courseSearchRequest).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Results.FirstOrDefault()?.CourseName, result.Results?.FirstOrDefault()?.CourseName);
            Assert.Equal(expectedResponse.Results?.FirstOrDefault()?.CourseRunId, result.Results?.FirstOrDefault()?.CourseRunId);
            Assert.Equal(expectedResponse.Results?.FirstOrDefault()?.CourseId, result.Results?.FirstOrDefault()?.CourseId);

            httpResponse.Dispose();
            httpClient.Dispose();
            fakeHttpMessageHandler.Dispose();
        }

        [Fact]
        public async Task CourseSearchAsyncReturnsHttpRequestExceptionWhenApiReturnsNotSuccessful()
        {
            // Arrange
            var courseSearchRequest = new CourseSearchRequest { SubjectKeyword = "Somekeyword" };

            var httpResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent("{}") };

            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            var auditService = A.Fake<IAuditService>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri("http://SomeDummyUrl") };
            var httpClientService = A.Fake<IHttpClientService>();
            A.CallTo(() => httpClientService.GetClient()).Returns(httpClient);
            var findACourseClient = new FindACourseClient(httpClientService, defaultSettings, auditService, defaultLogger);

            // Act
            await Assert.ThrowsAsync<HttpRequestException>(async () => await findACourseClient.CourseSearchAsync(courseSearchRequest).ConfigureAwait(false)).ConfigureAwait(false);

            // Assert
            httpResponse.Dispose();
            httpClient.Dispose();
            fakeHttpMessageHandler.Dispose();
        }
    }
}