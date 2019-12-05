using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClientV2.IntegrationTests
{
    [Trait("Course Search Client", "Integration Tests")]
    public class FindACourseClientTests
    {
        [Fact]
        public async Task GetCoursesAsync()
        {
            var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .Build();

            var courseSearchClientSettings = new CourseSearchClientSettings
            {
                CourseSearchSvcSettings = configuration.GetSection("Configuration:CourseSearchClient:CourseSearchSvc").Get<CourseSearchSvcSettings>() ?? new CourseSearchSvcSettings(),
                CourseSearchAuditCosmosDbSettings = configuration.GetSection("Configuration:CourseSearchClient:CosmosAuditConnection").Get<CourseSearchAuditCosmosDbSettings>() ?? new CourseSearchAuditCosmosDbSettings(),
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton(courseSearchClientSettings)
                .AddSingleton<IFindACourseClient, FindACourseClient>()
                .AddFindACourseServices(courseSearchClientSettings);

            serviceProvider.AddHttpClient<IFindACourseClient, FindACourseClient>().AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.RetryAsync(2));

            var services = serviceProvider.BuildServiceProvider();

            var findACourseClient = services.GetService<IFindACourseClient>();
            var courseSearchRequest = new CourseSearchRequest()
            {
                SubjectKeyword = configuration.GetSection("CourseSearch:KeyWordsForTest").Get<string>(),
                Start = 0,
                Limit = 5,
            };
            var searchResponse = await findACourseClient.CourseSearchAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Total.Should().BeGreaterThan(0);

            var course = searchResponse.Results.FirstOrDefault();

            //Get Details for a course
            var courseGetRequest = new CourseGetRequest()
            {
                CourseId = course.CourseId,
                RunId = course.CourseRunId,
            };

            var detailsResponse = await findACourseClient.CourseGetAsync(courseGetRequest).ConfigureAwait(false);

            detailsResponse.Course.CourseId.Should().Be(course.CourseId);
        }
    }
}