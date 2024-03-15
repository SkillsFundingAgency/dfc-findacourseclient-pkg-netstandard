using Autofac;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                    cfg.AddProfile(typeof(TLevelDetailsProfile));
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
                Filters = new CourseSearchFilters { SearchTerm = "Driving Test Theory", LearningMethod = LearningMethod.BlendedLearning },
                OrderedBy = CourseSearchOrderBy.Relevance,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CourseSearch_NonLars_Course()
        {
            var courseSearchRequest = new CourseSearchProperties
            {
                Filters = new CourseSearchFilters { SearchTerm = "Driving", SectorIds = new List<int> { 4, 8 }, CourseType = CourseType.SkillsBootcamp, EducationLevel = EducationLevel.Two },
                OrderedBy = CourseSearchOrderBy.Relevance,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CourseSearchForTLevels()
        {
            var courseSearchRequest = new CourseSearchProperties
            {
                Filters = new CourseSearchFilters { SearchTerm = "T Level" },
                OrderedBy = CourseSearchOrderBy.Relevance,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CompositeCourseSearch()
        {
            var courseSearchRequest = new CUIModels.CourseSearchProperties
            {
                Filters = new CUIModels.CourseSearchFilters { SearchTerm = "T Level" },
                OrderedBy = CUIModels.CourseSearchOrderBy.RecentlyAdded,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CompositeCourseSearch_NonLars()
        {
            var courseSearchRequest = new CUIModels.CourseSearchProperties
            {
                Filters = new CUIModels.CourseSearchFilters { SearchTerm = "Test", CourseType = new List<CUIModels.CourseType> { CUIModels.CourseType.SkillsBootcamp } },
                OrderedBy = CUIModels.CourseSearchOrderBy.RecentlyAdded,
            };

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CompositeTLevelSDetails()
        {
            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var tLevelResponse = await courseSearchService.GetTLevelDetailsAsync("01baf1e4-6ee0-497a-86c4-04bb1c11a6b9").ConfigureAwait(false);

            tLevelResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task CompositeCourseLongLatSearch()
        {
            var courseSearchRequest = new CUIModels.CourseSearchProperties
            {
                //Location for Birmingham
                Filters = new CUIModels.CourseSearchFilters { Longitude = -1.877556, Latitude = 52.468725, Distance = 5, DistanceSpecified = true },
            };
            courseSearchRequest.Filters.LearningMethod.Add(CUIModels.LearningMethod.ClassroomBased);

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
            searchResponse.Courses.OrderByDescending(c => c.LocationDetails.Distance).FirstOrDefault().Location.Should().Contain("Birmingham");
        }

        [Fact]
        public async Task CompositeCoursePostCode()
        {
            var courseSearchRequest = new CUIModels.CourseSearchProperties
            {
                //Postcode for Newcastle
                Filters = new CUIModels.CourseSearchFilters { PostCode = "NE1 1AD", Distance = 5, DistanceSpecified = true },
            };
            courseSearchRequest.Filters.LearningMethod.Add(CUIModels.LearningMethod.ClassroomBased);

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
            searchResponse.Courses.OrderBy(c => c.LocationDetails.Distance).FirstOrDefault().Location
                .ToUpper(System.Globalization.CultureInfo.InvariantCulture).Should().Contain("NEWCASTLE");
        }

        [Fact]
        public async Task CompositeCourseTownSearch()
        {
            var courseSearchRequest = new CUIModels.CourseSearchProperties
            {
                Filters = new CUIModels.CourseSearchFilters { Town = "Derby", Distance = 5, DistanceSpecified = true },
            };
            courseSearchRequest.Filters.LearningMethod.Add(CUIModels.LearningMethod.ClassroomBased);

            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);

            searchResponse.Should().NotBeNull();
            searchResponse.Courses.OrderByDescending(c => c.LocationDetails.Distance).FirstOrDefault().Location
                .ToUpper(System.Globalization.CultureInfo.InvariantCulture).Should().Contain("DERBY");
        }

        [Fact]
        public async Task GetSectors()
        {
            var courseSearchService = new CourseSearchApiService(findACourseClient, auditService, mapper);
            var sectors = await courseSearchService.GetSectorsAsync().ConfigureAwait(false);

            sectors.Should().NotBeNull();
        }
    }
}
