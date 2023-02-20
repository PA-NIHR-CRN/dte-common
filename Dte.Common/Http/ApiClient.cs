using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Dte.Common.Contracts;
using Dte.Common.Exceptions;
using Dte.Common.Helpers;
using Dte.Common.Responses;
using Newtonsoft.Json;

namespace Dte.Common.Http
{
    public class ApiClient : IDisposable, IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ApiClientConfiguration _apiClientConfiguration;
        private readonly string _serviceName;
        private bool _disposed;

        public ApiClient(HttpClient httpClient, ApiClientConfiguration apiClientConfiguration, string serviceName)
        {
            _httpClient = httpClient;
            _apiClientConfiguration = apiClientConfiguration;
            _serviceName = serviceName;
        }

        public void SetDefaultRequestHeaders(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Remove(name);
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public void RemoveDefaultRequestHeader(string name) => _httpClient.DefaultRequestHeaders.Remove(name);
        public void ClearDefaultRequestHeader() => _httpClient.DefaultRequestHeaders.Clear();

        public IApiResponse<string> Get(string uri, ContentNegotiation contentNegotiation = null) => SendRequest(HttpMethod.Get, uri, null, contentNegotiation).Result;

        public async Task<IApiResponse<string>> GetAsync(string uri, ContentNegotiation contentNegotiation = null)
        {
            return await SendRequest(HttpMethod.Get, uri, null, contentNegotiation);
        }

        public IApiResponse<TResponse> Get<TResponse>(string uri, ContentNegotiation contentNegotiation = null)
        {
            return SendRequest<object, TResponse>(HttpMethod.Get, uri, null, contentNegotiation).Result;
        }

        public async Task<IApiResponse<TResponse>> GetAsync<TResponse>(string uri, ContentNegotiation contentNegotiation = null)
        {
            return await SendRequest<object, TResponse>(HttpMethod.Get, uri, null, contentNegotiation);
        }

        public IApiResponse<string> Post(string uri, string request = null, ContentNegotiation contentNegotiation = null)
        {
            return SendRequest(HttpMethod.Post, uri, request, contentNegotiation).Result;
        }

        public Task<IApiResponse<string>> PostAsync(string uri, string request = null, ContentNegotiation contentNegotiation = null)
        {
            return SendRequest(HttpMethod.Post, uri, request, contentNegotiation);
        }

        public IApiResponse<TResponse> Post<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class
        {
            return SendRequest<TRequest, TResponse>(HttpMethod.Post, uri, request, contentNegotiation).Result;
        }

        public Task<IApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class
        {
            return SendRequest<TRequest, TResponse>(HttpMethod.Post, uri, request, contentNegotiation);
        }

        public IApiResponse<string> Put(string uri, string request = null, ContentNegotiation contentNegotiation = null)
        {
            return SendRequest(HttpMethod.Put, uri, request, contentNegotiation).Result;
        }

        public Task<IApiResponse<string>> PutAsync(string uri, string request = null, ContentNegotiation contentNegotiation = null)
        {
            return SendRequest(HttpMethod.Put, uri, request, contentNegotiation);
        }

        public IApiResponse<TResponse> Put<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class
        {
            return SendRequest<TRequest, TResponse>(HttpMethod.Put, uri, request, contentNegotiation).Result;
        }

        public Task<IApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class
        {
            return SendRequest<TRequest, TResponse>(HttpMethod.Put, uri, request, contentNegotiation);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClient?.Dispose();
            }

            _disposed = true;
        }

        public Task<IApiResponse<TResponse>> SendRequest<TRequest, TResponse>(HttpMethod method, string requestUri, TRequest request, ContentNegotiation contentNegotiation, ISerializer serializer) where TRequest : class
        {
            return ProcessRequest(GenerateRequestMessage(method, requestUri, CreateContent(request, serializer), contentNegotiation), async responseMessage =>
            {
                var stringContent = await responseMessage.Content.ReadAsStringAsync();
                serializer.TryDeserialize(stringContent, out TResponse content);

                return content;
            });
        }

        public Task<IApiResponse<TResponse>> SendRequest<TRequest, TResponse>(HttpMethod method, string requestUri, TRequest request, ContentNegotiation contentNegotiation) where TRequest : class
        {
            var serializer = SerializationFactory.GetSerializer(contentNegotiation?.ContentType, _apiClientConfiguration.ContractPropertyCase);

            return SendRequest<TRequest, TResponse>(method, requestUri, request, contentNegotiation, serializer);
        }

        public async Task<IApiResponse<string>> SendRequest(HttpMethod method, string requestUri, string request, ContentNegotiation contentNegotiation)
        {
            return await ProcessRequest(GenerateRequestMessage(method, requestUri, request, contentNegotiation), async responseMessage => await responseMessage.Content.ReadAsStringAsync());
        }

        private async Task<IApiResponse<TResponse>> ProcessRequest<TResponse>(HttpRequestMessage request, Func<HttpResponseMessage, Task<TResponse>> postProcessor)
        {
            await TraceRequest(request);

            var watch = Stopwatch.StartNew();

            HttpResponseMessage responseMessage;

            try
            {
                responseMessage = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpServiceException(_serviceName, request, ex, HttpStatusCode.InternalServerError);
            }
            catch (TaskCanceledException ex)
            {
                throw new HttpServiceException(_serviceName, request, ex, HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                throw new HttpServiceException(_serviceName, request, ex, HttpStatusCode.InternalServerError);
            }
            finally
            {
                watch.Stop();
            }

            var apiResponse = await BuildResponse(postProcessor, responseMessage);
            TraceResponse(responseMessage, apiResponse.StringContent, watch.Elapsed);

            return apiResponse;
        }

        private static async Task<IApiResponse<TResponse>> BuildResponse<TResponse>(Func<HttpResponseMessage, Task<TResponse>> postProcessor, HttpResponseMessage responseMessage)
        {
            var stringContent = await responseMessage.Content.ReadAsStringAsync();
            var apiResponse = ApiResponse<TResponse>.FromResponseMessage(responseMessage, await postProcessor(responseMessage), stringContent);

            return apiResponse;
        }

        private static string CreateContent<TRequest>(TRequest request, ISerializer serializer) where TRequest : class => request != null ? serializer.Serialize(request) : null;

        private HttpRequestMessage GenerateRequestMessage(HttpMethod method, string requestUri, string stringContent, ContentNegotiation contentNegotiation)
        {
            var requestMessage = new HttpRequestMessage(method, requestUri);
            ApplyContentNegotiation(requestMessage, contentNegotiation);
            var contentType = contentNegotiation?.ContentType;
            requestMessage.Content = string.IsNullOrEmpty(stringContent) ? (HttpContent)null : new StringContent(stringContent, Encoding.UTF8, contentType ?? "application/json");

            return requestMessage;
        }

        private static void ApplyContentNegotiation(HttpRequestMessage requestMessage, ContentNegotiation contentNegotiation)
        {
            if (contentNegotiation == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(contentNegotiation.Accept))
            {
                requestMessage.Headers.Remove("Accept");
                requestMessage.Headers.Add("Accept", contentNegotiation.Accept);
            }

            foreach (var (key, value) in contentNegotiation.Headers)
            {
                requestMessage.Headers.Remove(key);
                requestMessage.Headers.TryAddWithoutValidation(key, value);
            }
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