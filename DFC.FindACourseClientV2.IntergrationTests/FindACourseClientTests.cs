using DFC.FindACourseClientV2;
using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Models.APIRequests;
using DFC.FindACourseClientV2.Models.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Xunit;

namespace DFC.FindACourse.IntegrationTests
{
    [Trait("Course Search Client", "Intergration Tests")]
    public class FindACourseClientTests
    {
        [Fact]
        public void GetCoursesAsync()
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
                PageNo = 0,
                TopResults = 5,
            };
            var searchResponse = findACourseClient.CourseSearchAsync(courseSearchRequest);

            searchResponse.Result.Total.Should().BeGreaterThan(0);
        }
    }
}
