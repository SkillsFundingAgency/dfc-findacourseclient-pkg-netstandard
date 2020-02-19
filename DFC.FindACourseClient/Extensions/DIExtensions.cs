using Autofac;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Polly.Registry;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace DFC.FindACourseClient
{
    [ExcludeFromCodeCoverage]
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
                .SingleInstance();

            builder.Register(c => new FindACourseClient(
                c.ResolveNamed<HttpClient>(nameof(IFindACourseClient)),
                c.Resolve<CourseSearchClientSettings>(),
                c.Resolve<IAuditService>(),
                c.ResolveOptional<ILogger<IFindACourseClient>>()))
            .As<IFindACourseClient>()
            .InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var courseSearchClientSettings = c.Resolve<CourseSearchClientSettings>();
                return new DocumentClient(courseSearchClientSettings.CourseSearchAuditCosmosDbSettings.EndpointUrl, courseSearchClientSettings.CourseSearchAuditCosmosDbSettings.AccessKey);
            })
            .Named<IDocumentClient>(nameof(IFindACourseClient))
            .SingleInstance();

            builder.Register(c => new CosmosRepository<ApiAuditRecordCourse>(
                c.Resolve<CourseSearchClientSettings>().CourseSearchAuditCosmosDbSettings,
                c.ResolveNamed<IDocumentClient>(nameof(IFindACourseClient))))
            .As<ICosmosRepository<ApiAuditRecordCourse>>()
            .InstancePerLifetimeScope()
            .OnActivated(ctx => ctx.Instance.InitialiseDatabaseAsync(ctx.Context.Resolve<CourseSearchClientSettings>()
                .CourseSearchAuditCosmosDbSettings
                .Environment?
                .ToLowerInvariant()
                .Contains(Constants.LocalEnvironment)).GetAwaiter().GetResult());
        }

        //Use this method to configure all serivice for the client, including tranisent fault handling.
        //This will not work if you have other policies that need to be added to the policy registry, then use the split method AddFindACourseServicesWithoutFaultHandling below
        //Create your own shared services.AddPolicyRegistry() and pass it into the AddFindACourseTransientFaultHandlingPolicies.
        public static IServiceCollection AddFindACourseServices(this IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings)
        {
            AddCoreServices(services, courseSearchClientSettings);

            AddFindACourseTransientFaultHandlingPolicies(services, courseSearchClientSettings);

            return services;
        }

        public static IServiceCollection AddFindACourseServicesWithoutFaultHandling(this IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings)
        {
            AddCoreServices(services, courseSearchClientSettings);
            return services;
        }

        public static IServiceCollection AddFindACourseTransientFaultHandlingPolicies(this IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings, IPolicyRegistry<string> policyRegistry = null)
        {
            var policyOptions = courseSearchClientSettings?.PolicyOptions;
 
            if (policyRegistry == null)
            {
                policyRegistry = services.AddPolicyRegistry();
            }

            services.AddPolicies(policyRegistry, nameof(CourseSearchClientSettings), policyOptions)
            .AddHttpClient<IFindACourseClient, FindACourseClient>(courseSearchClientSettings?.CourseSearchSvcSettings, nameof(CourseSearchClientSettings), nameof(PolicyOptions.HttpRetry), nameof(PolicyOptions.HttpCircuitBreaker));

            return services;
        }

        private static void AddCoreServices(IServiceCollection services, CourseSearchClientSettings courseSearchClientSettings)
        {
            services.AddSingleton(courseSearchClientSettings);

            if (courseSearchClientSettings?.CourseSearchAuditCosmosDbSettings?.DatabaseId != null)
            {
                services.AddScoped<ICosmosRepository<ApiAuditRecordCourse>, CosmosRepository<ApiAuditRecordCourse>>(s =>
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
        }
    }
}