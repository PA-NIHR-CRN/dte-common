using System.Net.Http;
using System.Threading.Tasks;
using Dte.Common.Helpers;
using Dte.Common.Responses;

namespace Dte.Common.Contracts
{
    public interface IApiClient
    {
        void SetDefaultRequestHeaders(string name, string value);
        void RemoveDefaultRequestHeader(string name);
        void ClearDefaultRequestHeader();
        IApiResponse<string> Get(string uri, ContentNegotiation contentNegotiation = null);
        IApiResponse<TResponse> Get<TResponse>(string uri, ContentNegotiation contentNegotiation = null);
        Task<IApiResponse<string>> GetAsync(string uri, ContentNegotiation contentNegotiation = null);
        Task<IApiResponse<TResponse>> GetAsync<TResponse>(string uri, ContentNegotiation contentNegotiation = null);
        IApiResponse<string> Post(string uri, string request = null, ContentNegotiation contentNegotiation = null);
        IApiResponse<TResponse> Post<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class;
        Task<IApiResponse<string>> PostAsync(string uri, string request = null, ContentNegotiation contentNegotiation = null);
        Task<IApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class;
        IApiResponse<string> Put(string uri, string request = null, ContentNegotiation contentNegotiation = null);
        IApiResponse<TResponse> Put<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class;
        Task<IApiResponse<string>> PutAsync(string uri, string request = null, ContentNegotiation contentNegotiation = null);
        Task<IApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest request = null, ContentNegotiation contentNegotiation = null) where TRequest : class;
        void Dispose();
        Task<IApiResponse<TResponse>> SendRequest<TRequest, TResponse>(HttpMethod method, string requestUri, TRequest request, ContentNegotiation contentNegotiation, ISerializer serializer) where TRequest : class;
        Task<IApiResponse<TResponse>> SendRequest<TRequest, TResponse>(HttpMethod method, string requestUri, TRequest request, ContentNegotiation contentNegotiation) where TRequest : class;
        Task<IApiResponse<string>> SendRequest(HttpMethod method, string requestUri, string request, ContentNegotiation contentNegotiation);
    }
}