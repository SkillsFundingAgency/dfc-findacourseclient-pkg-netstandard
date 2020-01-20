using AutoMapper;
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

            serviceProvider.AddSingleton(sp =>
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(typeof(FindACourseProfile));
                }).CreateMapper();
            });

            var services = serviceProvider.BuildServiceProvider();

            findACourseClient = services.GetService<IFindACourseClient>();
            auditService = services.GetService<IAuditService>();
            mapper = services.GetService<IMapper>();
        }

        [Fact]
        public async Task CourseGet()
        {
            var courseGetRequest = new CourseGetRequest
            {
                CourseId = Guid.Parse("a7533f3a-4feb-4b91-bc1d-2a9d69ff12d3"),
                RunId = Guid.Parse("ffba6c5b-2155-45e8-a376-9f6d790ed99f"),
            };
            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var detailResponse = await courseSearchService.GetCourseDetailsAsync(courseGetRequest.CourseId.ToString(), courseGetRequest.RunId.ToString()).ConfigureAwait(false);

            detailResponse.CourseId.Should().Be(courseGetRequest.CourseId.ToString());
        }

        [Fact]
        public async Task CourseSearch()
        {
            var courseSearchRequest = new CourseSearchProperties
            {
                Filters = new CourseSearchFilters { SearchTerm = "biology" },
                OrderedBy = CourseSearchOrderBy.Distance,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);
        }
    }
}