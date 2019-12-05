using DFC.FindACourseClient;
using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Models.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .AddSingleton<ICourseSearchClient, CourseSearchClient>()
                .AddFindACourseServices(courseSearchClientSettings)
                .BuildServiceProvider();

            var courseSearchClient = serviceProvider.GetService<ICourseSearchClient>();
            var results = courseSearchClient.GetCoursesAsync(configuration.GetSection("CourseSearch:KeyWordsForTest").Get<string>());

            results.Result.Count().Should().BeGreaterThan(0);
        }
    }
}
