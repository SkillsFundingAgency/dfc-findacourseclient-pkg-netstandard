using DFC.FindACourseClientV2.Contracts;
using DFC.FindACourseClientV2.Contracts.CosmosDb;
using DFC.FindACourseClientV2.Models.CosmosDb;
using DFC.FindACourseClientV2.Repositories;
using DFC.FindACourseClientV2.Services;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DFC.FindACourseClientV2.Models.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddFindACourseServices(this IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings)
        {
            if (courseSearchClientSettings?.CourseSearchAuditCosmosDbSettings?.DatabaseId != null)
            {
                services.AddSingleton<ICosmosRepository<ApiAuditRecordCourse>, CosmosRepository<ApiAuditRecordCourse>>(s =>
                {
                    var cosmosDbAuditConnection = courseSearchClientSettings.CourseSearchAuditCosmosDbSettings;
                    var documentClient = new DocumentClient(cosmosDbAuditConnection.EndpointUrl, cosmosDbAuditConnection.AccessKey);
                    return new CosmosRepository<ApiAuditRecordCourse>(cosmosDbAuditConnection, documentClient);
                });

                services.AddSingleton<IFindACourseClient, FindACourseClient>();
                services.AddSingleton<IAuditService, AuditService>();
            }

            return services;
        }
    }
}