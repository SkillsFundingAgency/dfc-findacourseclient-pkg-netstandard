using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Models.CosmosDb;
using DFC.FindACourseClient.Repository.CosmosDb;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;

namespace DFC.FindACourseClient.Models.Configuration
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddFindACourseServices(this IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings)
        {
            if (courseSearchClientSettings?.CourseSearchAuditCosmosDbSettings?.DatabaseId != null)
            {
                services.AddSingleton<ICosmosRepository<APIAuditRecordCourse>, CosmosRepository<APIAuditRecordCourse>>(s =>
                {
                    var cosmosDbAuditConnection = courseSearchClientSettings.CourseSearchAuditCosmosDbSettings;
                    var documentClient = new DocumentClient(cosmosDbAuditConnection.EndpointUrl, cosmosDbAuditConnection.AccessKey);
                    return new CosmosRepository<APIAuditRecordCourse>(cosmosDbAuditConnection, documentClient);
                });
            }

            services.AddSingleton<IServiceClientProxy, ServiceClientProxy>();
            services.AddSingleton<IMessageConverter, MessageConverter>();

            return services;
        }
    }
}
