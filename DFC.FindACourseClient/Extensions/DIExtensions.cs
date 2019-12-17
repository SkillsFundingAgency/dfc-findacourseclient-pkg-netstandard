using Autofac;
using AutoMapper;
using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Extensions;
using DFC.FindACourseClient.HttpClientPolicies;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.Models.CosmosDb;
using DFC.FindACourseClient.Repositories;
using DFC.FindACourseClient.Services;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;

namespace DFC.FindACourseClient
{
    public static class DIExtensions
    {
        public static void RegisterFindACourseClientSdk(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(DIExtensions).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(c =>
                {
                    var courseSearchClientSettings = c.Resolve<CourseSearchClientSettings>();
                    var client = new HttpClient
                    {
                        BaseAddress = courseSearchClientSettings.CourseSearchSvcSettings.ServiceEndpoint,
                        Timeout = new TimeSpan(0, 0, 0, courseSearchClientSettings.CourseSearchSvcSettings.RequestTimeOutSeconds),
                    };
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                    client.DefaultRequestHeaders.Add(Constants.ApimSubscriptionKey, courseSearchClientSettings.CourseSearchSvcSettings.ApiKey);

                    return client;
                })
                .Named<HttpClient>(nameof(IFindACourseClient))
                .InstancePerLifetimeScope();

            builder.Register(c => new FindACourseClient(
                c.ResolveNamed<HttpClient>(nameof(IFindACourseClient)),
                c.Resolve<CourseSearchClientSettings>(),
                c.Resolve<IAuditService>(),
                c.Resolve<ILogger>()))
            .As<IFindACourseClient>()
            .InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var courseSearchClientSettings = c.Resolve<CourseSearchClientSettings>();
                return new DocumentClient(courseSearchClientSettings.CourseSearchAuditCosmosDbSettings.EndpointUrl, courseSearchClientSettings.CourseSearchAuditCosmosDbSettings.AccessKey);
            })
            .Named<IDocumentClient>(nameof(IFindACourseClient))
            .InstancePerLifetimeScope();

            builder.Register(c => new CosmosRepository<ApiAuditRecordCourse>(
                c.Resolve<CourseSearchClientSettings>().CourseSearchAuditCosmosDbSettings,
                c.ResolveNamed<IDocumentClient>(nameof(IFindACourseClient))))
            .As<ICosmosRepository<ApiAuditRecordCourse>>()
            .InstancePerLifetimeScope()
            .OnActivated(async ctx => await ctx.Instance.InitialiseDatabaseAsync(ctx.Context.Resolve<CourseSearchClientSettings>()
                .CourseSearchAuditCosmosDbSettings
                .Environment?
                .ToLowerInvariant()
                .Contains(Constants.LocalEnvironment)));
        }

        public static IServiceCollection AddFindACourseServices(this IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings)
        {
            services.AddSingleton(courseSearchClientSettings);

            if (courseSearchClientSettings?.CourseSearchAuditCosmosDbSettings?.DatabaseId != null)
            {
                services.AddSingleton<ICosmosRepository<ApiAuditRecordCourse>, CosmosRepository<ApiAuditRecordCourse>>(s =>
                {
                    var cosmosDbAuditConnection = courseSearchClientSettings.CourseSearchAuditCosmosDbSettings;
                    var documentClient = new DocumentClient(cosmosDbAuditConnection.EndpointUrl, cosmosDbAuditConnection.AccessKey);
                    var isDevelopmentEnvironment = courseSearchClientSettings.CourseSearchAuditCosmosDbSettings.Environment?.ToLowerInvariant().Contains(Constants.LocalEnvironment);
                    var repository = new CosmosRepository<ApiAuditRecordCourse>(cosmosDbAuditConnection, documentClient);
                    repository.InitialiseDatabaseAsync(isDevelopmentEnvironment.GetValueOrDefault()).GetAwaiter().GetResult();
                    return repository;
                });
            }

            services.AddScoped<IFindACourseClient, FindACourseClient>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddAutoMapper(typeof(DIExtensions).Assembly);

            var policyOptions = courseSearchClientSettings.PolicyOptions;
            var policyRegistry = services.AddPolicyRegistry();

            services
                .AddPolicies(policyRegistry, nameof(CourseSearchClientSettings), policyOptions)
                .AddHttpClient<IFindACourseClient, FindACourseClient>(courseSearchClientSettings.CourseSearchSvcSettings, nameof(CourseSearchClientSettings), nameof(PolicyOptions.HttpRetry), nameof(PolicyOptions.HttpCircuitBreaker));

            return services;
        }
    }
}