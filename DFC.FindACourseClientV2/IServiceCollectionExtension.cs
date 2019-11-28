using DFC.FindACourseClientV2.Contracts.CosmosDb;
using DFC.FindACourseClientV2.Models.CosmosDb;
using DFC.FindACourseClientV2.Repository.CosmosDb;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;

namespace DFC.FindACourseClientV2.Models.Configuration
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
            return services;
        }
    }
}
