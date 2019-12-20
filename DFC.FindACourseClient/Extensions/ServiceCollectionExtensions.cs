using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using System;

namespace DFC.FindACourseClient
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddPolicies(
            this IServiceCollection services,
            IPolicyRegistry<string> policyRegistry,
            string keyPrefix,
            PolicyOptions policyOptions)
        {
            policyRegistry.Add(
                $"{keyPrefix}_{nameof(PolicyOptions.HttpRetry)}",
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(
                        policyOptions.HttpRetry.Count,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(policyOptions.HttpRetry.BackoffPower, retryAttempt))));

            policyRegistry.Add(
                $"{keyPrefix}_{nameof(PolicyOptions.HttpCircuitBreaker)}",
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: policyOptions.HttpCircuitBreaker.ExceptionsAllowedBeforeBreaking,
                        durationOfBreak: policyOptions.HttpCircuitBreaker.DurationOfBreak));

            return services;
        }

        internal static IServiceCollection AddHttpClient<TClient, TImplementation>(
                    this IServiceCollection services,
                    CourseSearchSvcSettings courseSearchSvcSettings,
                    string configurationSectionName,
                    string retryPolicyName,
                    string circuitBreakerPolicyName)
                    where TClient : class
                    where TImplementation : class, TClient =>
                    services
                        .AddHttpClient<TClient, TImplementation>()
                        .ConfigureHttpClient((sp, options) =>
                        {
                            options.BaseAddress = courseSearchSvcSettings.ServiceEndpoint;
                            options.Timeout = new TimeSpan(0, 0, 0, courseSearchSvcSettings.RequestTimeOutSeconds);
                            options.DefaultRequestHeaders.Clear();
                            options.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                            options.DefaultRequestHeaders.Add(Constants.ApimSubscriptionKey, courseSearchSvcSettings.ApiKey);
                        })
                        .AddPolicyHandlerFromRegistry($"{configurationSectionName}_{retryPolicyName}")
                        .AddPolicyHandlerFromRegistry($"{configurationSectionName}_{circuitBreakerPolicyName}")
                        .Services;
    }
}