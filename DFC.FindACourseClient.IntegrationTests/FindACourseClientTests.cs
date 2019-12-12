using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Models.APIRequests;
using DFC.FindACourseClient.Models.Configuration;
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

            var serviceProvider = new ServiceCollection()
                .AddFindACourseServices(configuration);

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