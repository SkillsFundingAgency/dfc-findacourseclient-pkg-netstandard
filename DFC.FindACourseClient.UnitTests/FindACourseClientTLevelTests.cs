using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.UnitTests.ClientHandlers;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests
{
    public class FindACourseClientTLevelTests
    {
        private readonly Guid tLevelId = Guid.NewGuid();
        private readonly CourseSearchClientSettings defaultSettings;
        private readonly ILogger<IFindACourseClient> defaultLogger;

        public FindACourseClientTLevelTests()
        {
            defaultLogger = A.Fake<ILogger<IFindACourseClient>>();
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
        public async Task TLevelGetAsyncReturnsResponseObjectWhenApiCallIsSuccessful()
        {
            // Arrange
            var expectedResponse = new TLevelDetailResponse
            {
                TLevelId = tLevelId,
                Qualification = new TLevelQualification() { TLevelName = "testTLevel" },
                Provider = new TLevelProvider() { ProviderName = "testProvider" },
            };

            using var httpResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) };

            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            var auditService = A.Fake<IAuditService>();

            using var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            using var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri("http://SomeDummyUrl") };

            var findACourseClient = new FindACourseClient(httpClient, defaultSettings, auditService, defaultLogger);

            // Act
            var result = await findACourseClient.TLevelGetAsync(tLevelId.ToString()).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Qualification.TLevelName, result.Qualification.TLevelName);
            Assert.Equal(expectedResponse.Provider.ProviderName, result.Provider.ProviderName);
            Assert.Equal(expectedResponse.TLevelId, result.TLevelId);
        }

        [Fact]
        public async Task TLevelGetAsyncCatchesExceptionWhenApiReturnsNotSuccessful()
        {
            // Arrange
            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Throws<Exception>();

            var auditService = A.Fake<IAuditService>();

            using var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            using var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri("http://SomeDummyUrl") };
            var findACourseClient = new FindACourseClient(httpClient, defaultSettings, auditService, defaultLogger);

            // Act
            var result = await findACourseClient.TLevelGetAsync(tLevelId.ToString()).ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }
    }
}
