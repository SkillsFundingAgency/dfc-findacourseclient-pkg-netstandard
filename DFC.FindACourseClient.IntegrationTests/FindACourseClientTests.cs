using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.IntegrationTests
{
    [Trait("Course Search Client", "Integration Tests")]
    public class FindACourseClientTests
    {
        private readonly IConfigurationRoot configuration;
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
                PolicyOptions = configuration.GetSection("Configuration:CourseSearchClient:Policies").Get<PolicyOptions>() ?? new PolicyOptions(),
            };

            var serviceProvider = new ServiceCollection()
                .AddFindACourseServices(courseSearchClientSettings);

            var services = serviceProvider.BuildServiceProvider();

            findACourseClient = services.GetService<IFindACourseClient>();
        }

        [Fact]
        public async Task CourseSearch()
        {
            var courseSearchRequest = new CourseSearchRequest
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
            var courseGetRequest = new CourseGetRequest
            {
                CourseId = Guid.Parse("36d9dfb8-7fa9-4779-beae-ca65ba429bfd"),
                RunId = Guid.Parse("4776fd59-f313-47c9-8fea-5f9cbc58bcd4"),
            };

            var detailsResponse = await findACourseClient.CourseGetAsync(courseGetRequest).ConfigureAwait(false);

            detailsResponse.Course.CourseId.Should().Be(courseGetRequest.CourseId.ToString());
        }

        [Fact]
        public async Task SectorsGet()
        {
            var sectorsResponse = await findACourseClient.SectorsGetAsync().ConfigureAwait(false);

            sectorsResponse.Should().NotBeNull();
            sectorsResponse.Count.Should().BeGreaterThan(0);
        }
    }
}