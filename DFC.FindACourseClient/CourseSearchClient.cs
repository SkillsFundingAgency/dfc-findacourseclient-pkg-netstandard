using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Models;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.Models.CosmosDb;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DFC.FindACourseClient
{
    public class CourseSearchClient : ICourseSearchClient
    {
        private readonly ILogger<CourseSearchClient> logger;
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private readonly ICosmosRepository<APIAuditRecordCourse> auditRepository;
        private readonly IServiceClientProxy serviceClientProxy;
        private readonly IMessageConverter messageConverter;

        public CourseSearchClient(CourseSearchClientSettings courseSearchClientSettings, IMessageConverter messageConverter, IServiceClientProxy serviceClientProxy, ILogger<CourseSearchClient> logger = null, ICosmosRepository<APIAuditRecordCourse> auditRepository = null)
        {
            this.logger = logger;
            this.courseSearchClientSettings = courseSearchClientSettings;
            this.auditRepository = auditRepository;
            this.serviceClientProxy = serviceClientProxy;
            this.messageConverter = messageConverter;
        }

        public async Task<IEnumerable<CourseSumary>> GetCoursesAsync(string courseSearchKeywords)
        {
            logger?.LogInformation($"{nameof(GetCoursesAsync)} has been called with keywords {courseSearchKeywords} ");
            var request = messageConverter.GetCourseListRequest(courseSearchKeywords, courseSearchClientSettings.CourseSearchSvcSettings);

            var binding = new BasicHttpsBinding(BasicHttpsSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            //Used to initialize the OperationTimeout, which governs the whole process of sending a message, including receiving a reply message for a request/ reply service operation.
            binding.SendTimeout = new TimeSpan(0, 0, 0, courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds);

            var endpoint = new EndpointAddress(courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint);
            var factory = new ChannelFactory<ServiceInterface>(binding, endpoint);

            var serviceInterfaceClient = serviceClientProxy.CreateChannel(factory);

            bool success = false;
            try
            {
                var courseListResult = await ((ServiceInterface)serviceInterfaceClient).CourseListAsync(request).ConfigureAwait(false);
                serviceInterfaceClient.Close();
                success = true;

                if (auditRepository != null)
                {
                    request.CourseListRequest.CourseSearchCriteria.APIKey = $"{request.CourseListRequest.CourseSearchCriteria.APIKey.Substring(0, 3)}-------removed in audit----------";
                    var auditRecord = new APIAuditRecordCourse() { DocumentId = Guid.NewGuid(), CorrelationId = Guid.NewGuid(), Request = request, Response = courseListResult };
                    await auditRepository.UpsertAsync(auditRecord).ConfigureAwait(false);
                }

                var convertedResults = messageConverter.ConvertToCourse(courseListResult);
                logger?.LogInformation($"{nameof(GetCoursesAsync)} has returned {convertedResults.Count()} courses for keywords {courseSearchKeywords} ");
                return convertedResults;
            }
            finally
            {
                if (!success)
                {
                    serviceInterfaceClient.Abort();
                }
            }
        }
    }
}
