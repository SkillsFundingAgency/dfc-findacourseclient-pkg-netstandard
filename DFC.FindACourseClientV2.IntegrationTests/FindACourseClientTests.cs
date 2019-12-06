using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClientV2.IntegrationTests
{
    [Trait("Course Search Client", "Integration Tests")]
    public class FindACourseClientTests
    {
        private readonly IConfiguration configuration;
        private readonly IFindACourseClient findACourseClient;

        public FindACourseClientTests()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var courseSearchClientSettings = new CourseSearchClientSettings
            {
                CourseSearchSvcSettings = configuration.GetSection("Configuration:CourseSearchClient:CourseSearchSvc").Get<CourseSearchSvcSettings>() ?? new CourseSearchSvcSettings(),
                CourseSearchAuditCosmosDbSettings = configuration.GetSection("Configuration:CourseSearchClient:CosmosAuditConnection").Get<CourseSearchAuditCosmosDbSettings>() ?? new CourseSearchAuditCosmosDbSettings(),
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton(courseSearchClientSettings)
                .AddFindACourseServices(courseSearchClientSettings);
            serviceProvider.AddHttpClient<IFindACourseClient, FindACourseClient>().AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(2));

            var services = serviceProvider.BuildServiceProvider();

            findACourseClient = services.GetService<IFindACourseClient>();
        }

        [Fact]
        public async Task CourseSearch()
        {
            var courseSearchRequest = new CourseSearchRequest()
            {
                SubjectKeyword = configuration.GetSection("CourseSearch:KeyWordsForTest").Get<string>(),
                Start = 0,
                Limit = 5,
            };
            var searchResponse = await findACourseClient.CourseSearchAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Total.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CourseGet()
        {
            //Get Details for a course
            var courseGetRequest = new CourseGetRequest()
            {
                CourseId = Guid.Parse("a4dcc053-67e7-462c-b3c1-52c3add949b4"),
                RunId = Guid.Parse("052f98d6-d294-4d8c-801b-33bb80fe60f9"),
            };

            var detailsResponse = await findACourseClient.CourseGetAsync(courseGetRequest).ConfigureAwait(false);

            detailsResponse.Course.CourseId.Should().Be("a4dcc053-67e7-462c-b3c1-52c3add949b4");
        }
    }
}