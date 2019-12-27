using Autofac;
using AutoMapper;
using DFC.FindACourseClient;
using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClientFramework.IntergrationTests
{
    public class CourseSearchServiceTests
    {
        private readonly IFindACourseClient findACourseClient;
        private readonly IAuditService auditService;
        private readonly IMapper mapper;
        private readonly IContainer container;

        public CourseSearchServiceTests()
        {
            // Set the security protocol
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var builder = new ContainerBuilder();

            builder.Register(c =>
            {
                return new CourseSearchClientSettings
                {
                    CourseSearchSvcSettings = new CourseSearchSvcSettings()
                    {
                        ServiceEndpoint = new Uri(this.GetConfig<string>(Constants.CourseSearchClientCourseSearchSvcServiceEndpoint)),
                        ApiKey = this.GetConfig<string>(Constants.CourseSearchClientCourseSearchSvcAPIKey),
                        SearchPageSize = this.GetConfig<string>(Constants.CourseSearchClientCourseSearchSvcSearchPageSize),
                        RequestTimeOutSeconds = this.GetConfig<int>(Constants.CourseSearchClientCourseSearchSvcRequestTimeOutSeconds),
                        TransintErrorsNumberOfRetries = this.GetConfig<int>(Constants.CourseSearchClientCourseSearchSvcTransintErrorsNumberOfRetries),
                    },
                    CourseSearchAuditCosmosDbSettings = new CourseSearchAuditCosmosDbSettings()
                    {
                        AccessKey = this.GetConfig<string>(Constants.CourseSearchClientCosmosAuditConnectionAccessKey),
                        EndpointUrl = new Uri(this.GetConfig<string>(Constants.CourseSearchClientCosmosAuditConnectionEndpointUrl)),
                        DatabaseId = this.GetConfig<string>(Constants.CourseSearchClientCosmosAuditConnectionDatabaseId),
                        CollectionId = this.GetConfig<string>(Constants.CourseSearchClientCosmosAuditConnectionCollectionId),
                        PartitionKey = this.GetConfig<string>(Constants.CourseSearchClientCosmosAuditConnectionPartitionKey),
                        Environment = this.GetConfig<string>(Constants.CourseSearchClientCosmosAuditConnectionEnvironment),
                    },
                };
            });

            builder.RegisterFindACourseClientSdk();
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FindACourseProfile>();
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
            this.container = builder.Build();
            this.findACourseClient = this.container.Resolve<IFindACourseClient>();
            this.auditService = this.container.Resolve<IAuditService>();
            this.mapper = this.container.Resolve<IMapper>();
        }

        [Fact]
        public async Task CourseSearch()
        {
            var courseSearchRequest = new CourseSearchProperties()
            {
                Filters = new CourseSearchFilters { SearchTerm = "biology", StartDate = StartDate.FromToday, StartDateFrom = DateTime.Today},
            };

            var courseSearchService = new CourseSearchApiService(this.findACourseClient, this.auditService, this.mapper);
            var searchResponse = await courseSearchService.SearchCoursesAsync(courseSearchRequest).ConfigureAwait(false);
        }

        private T GetConfig<T>(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}