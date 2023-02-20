using System;
using System.Net.Http;
using System.Text;
using Dte.Common.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Dte.Common.Extensions
{
    public static class HttpExtensions
    {
        public static IServiceCollection AddHttpClientWithRetry<TInterface, TImplementation>(this IServiceCollection services, ClientSettings clientSettings, int retryCount, ILogger logger)
            where TInterface : class where TImplementation : class, TInterface
        {
            services.AddHttpClient<TInterface, TImplementation>(c =>
                {
                    c.BaseAddress = new Uri(clientSettings.BaseUrl);
                    c.Timeout = clientSettings.DefaultTimeout;

                    if (string.IsNullOrWhiteSpace(clientSettings.UserName) || string.IsNullOrWhiteSpace(clientSettings.Password)) return;
                    
                    var authString = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(clientSettings.UserName + ":" + clientSettings.Password));
                    c.DefaultRequestHeaders.Add("Authorization", authString);
                })
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    //.OrResult(message => !message.IsSuccessStatusCode)
                    .WaitAndRetryAsync(retryCount, x => x * TimeSpan.FromSeconds(2), (result, timeSpan, count, context) => logger.LogWarning($"HTTP Error calling: {clientSettings.BaseUrl} ({result?.Result?.StatusCode}: Error:{result?.Exception?.Message}) - Retrying: {count}/{retryCount} - waiting {timeSpan.TotalSeconds} seconds")));

            return services;
        }
        
        public static IServiceCollection AddHttpClientWithRetry<TInterface, TImplementation, THttpMessageHandler>(this IServiceCollection services, ClientSettings clientSettings, int retryCount, ILogger logger)
            where TInterface : class where TImplementation : class, TInterface where THttpMessageHandler : DelegatingHandler
        {
            services.AddHttpClient<TInterface, TImplementation>(c =>
                {
                    c.BaseAddress = new Uri(clientSettings.BaseUrl);
                    c.Timeout = clientSettings.DefaultTimeout;
                    
                    if (string.IsNullOrWhiteSpace(clientSettings.UserName) || string.IsNullOrWhiteSpace(clientSettings.Password)) return;
                    
                    var authString = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(clientSettings.UserName + ":" + clientSettings.Password));
                    c.DefaultRequestHeaders.Add("Authorization", authString);
                })
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    //.OrResult(message => !message.IsSuccessStatusCode)
                    .WaitAndRetryAsync(retryCount, x => x * TimeSpan.FromSeconds(2), (result, timeSpan, count, context) => logger.LogWarning($"HTTP Error calling: {clientSettings.BaseUrl} ({result?.Result?.StatusCode}: Error:{result?.Exception?.Message}) - Retrying: {count}/{retryCount} - waiting {timeSpan.TotalSeconds} seconds")))
                .AddHttpMessageHandler<THttpMessageHandler>();

            return services;
        }
    }
}