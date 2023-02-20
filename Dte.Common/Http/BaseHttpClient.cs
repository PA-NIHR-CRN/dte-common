using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Dte.Common.Exceptions;
using Dte.Common.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dte.Common.Http
{
    public abstract class BaseHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseHttpClient> _logger;
        private readonly ApiClientConfiguration _apiClientConfiguration;

        protected abstract string ServiceName { get; }

        protected BaseHttpClient(HttpClient httpClient, IHeaderService headerService, ILogger<BaseHttpClient> logger, ApiClientConfiguration apiClientConfiguration)
        {
            _httpClient = httpClient;
            HeaderService = headerService;
            _logger = logger;
            _apiClientConfiguration = apiClientConfiguration;
        }

        protected readonly IHeaderService HeaderService;

        protected async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            HeaderService.AddAllHeadersToRequest(request);

            await TraceRequest(request);

            HttpResponseMessage response;

            var sw = Stopwatch.StartNew();

            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpServiceException(ServiceName, request, ex, HttpStatusCode.InternalServerError);
            }
            catch (TaskCanceledException ex)
            {
                throw new HttpServiceException(ServiceName, request, ex, HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                throw new HttpServiceException(ServiceName, request, ex, HttpStatusCode.InternalServerError);
            }

            var responseContent = response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;
            TraceResponse(response, responseContent, sw.Elapsed);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseContent);
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    HandleBadRequest(responseContent);
                    break;
                case HttpStatusCode.NotFound:
                    HandleNotFound(responseContent);
                    break;
                default:
                    _logger.LogError($"Received status code {response.StatusCode} ({(int)response.StatusCode}) when calling endpoint {request.RequestUri}");
                    break;
            }

            throw new HttpServiceException(ServiceName, request, response.StatusCode, responseContent);
        }

        protected virtual void HandleBadRequest(string content)
        {
        }

        protected virtual void HandleNotFound(string content)
        {
        }

        private async Task TraceRequest(HttpRequestMessage requestMessage)
        {
            _apiClientConfiguration.TraceWriter.WriteLine(JsonConvert.SerializeObject(new
            {
                ExternalRequest = new
                {
                    Url = $"{requestMessage.Method} {_httpClient.BaseAddress}{requestMessage.RequestUri}",
                    Content = requestMessage.Content != null ? await requestMessage.Content.ReadAsStringAsync() : null,
                    Headers = HttpHeadersHelper.GetHeadersArray(_httpClient.DefaultRequestHeaders, requestMessage.Headers, requestMessage.Content?.Headers)
                }
            }));
        }

        private void TraceResponse(HttpResponseMessage response, string stringContent, TimeSpan responseTime)
        {
            _apiClientConfiguration.TraceWriter.WriteLine(JsonConvert.SerializeObject(new
            {
                ExternalResponse = new
                {
                    Url = $"[{(int)response.StatusCode}] {response.StatusCode} from {response.RequestMessage?.RequestUri}",
                    response.IsSuccessStatusCode,
                    response.ReasonPhrase,
                    Took = responseTime,
                    Headers = HttpHeadersHelper.GetHeadersArray(response.Headers, response.Content.Headers)
                }
            }));
        }
    }
}