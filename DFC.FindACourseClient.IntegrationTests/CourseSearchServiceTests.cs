using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using CUIModels = DFC.CompositeInterfaceModels.FindACourseClient;

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
                CourseId = Guid.Parse("2ed8b2f9-da49-4432-963b-5842746aef5c"),
                RunId = Guid.Parse("f6f0036f-5cdf-486c-8b3e-1bb102940cd1"),
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
                Filters = new CourseSearchFilters { SearchTerm = "Surveying" },
                OrderedBy = CourseSearchOrderBy.Relevance,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CopositeCourseSearch()
        {
            var courseSearchRequest = new CUIModels.CourseSearchProperties
            {
                Filters = new CUIModels.CourseSearchFilters { SearchTerm = "T Level" },
                OrderedBy = CUIModels.CourseSearchOrderBy.Relevance,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CopositeTLevelSDetails()
        {
            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var tLevelResponse = await courseSearchService.GetTLevelDetailsAsync("00000000-0000-0000-0000-000000000000").ConfigureAwait(false);

            tLevelResponse.Should().NotBeNull();
        }
    }
}
