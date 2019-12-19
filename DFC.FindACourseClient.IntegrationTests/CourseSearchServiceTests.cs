using AutoMapper;
using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.HttpClientPolicies;
using DFC.FindACourseClient.Models.APIRequests;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.Models.ExternalInterfaceModels;
using DFC.FindACourseClient.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.IntegrationTests
{
    public class CourseSearchServiceTests
    {
        private readonly IConfigurationRoot configuration;
        private readonly IFindACourseClient findACourseClient;
        private readonly IAuditService auditService;
        private readonly IMapper mapper;

        public CourseSearchServiceTests()
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
            auditService = services.GetService<IAuditService>();
            mapper = services.GetService<IMapper>();
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
            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var detailResponse = await courseSearchService.GetCourseDetailsAsync(courseGetRequest.CourseId.ToString(), courseGetRequest.RunId.ToString()).ConfigureAwait(false);

            detailResponse.CourseId.Should().Be("a4dcc053-67e7-462c-b3c1-52c3add949b4");
        }

        [Fact]
        public async Task CourseSearch()
        {
            var courseSearchRequest = new CourseSearchProperties()
            {
                Filters = new CourseSearchFilters { SearchTerm = "biology" },
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);
        }
    }
}