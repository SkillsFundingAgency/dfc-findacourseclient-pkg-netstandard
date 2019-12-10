using AutoMapper;
using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.Models.CosmosDb;
using DFC.FindACourseClient.Repositories;
using DFC.FindACourseClient.Services;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace DFC.FindACourseClient
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
                    var isDevelopmentEnvironment = courseSearchClientSettings.CourseSearchAuditCosmosDbSettings.Environment?.ToLowerInvariant().Contains("development");
                    var repository = new CosmosRepository<ApiAuditRecordCourse>(cosmosDbAuditConnection, documentClient);
                    repository.InitialiseDatabaseAsync(isDevelopmentEnvironment.GetValueOrDefault()).GetAwaiter().GetResult();
                    return repository;
                });
            }

            services.AddSingleton<IFindACourseClient, FindACourseClient>();
            services.AddSingleton<IAuditService, AuditService>();
            services.AddTransient(provider =>
            {
                var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds),
                };
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", courseSearchClientSettings.CourseSearchSvcSettings.ApiKey);
                return httpClient;
            });

            services.AddAutoMapper(typeof(IServiceCollectionExtension).Assembly);

            return services;
            return services;
        }
    }
}