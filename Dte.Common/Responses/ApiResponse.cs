using System.Net;
using System.Net.Http;
using Dte.Common.Helpers;

namespace Dte.Common.Responses
{
    public abstract class ApiResponse
    {
        public HttpStatusCode StatusCode { get; protected set; }
        public HeaderCollection Headers { get; protected set; }
        public HeaderCollection ContentHeaders { get; protected set; }
        public string StringContent { get; protected set; }
        public T GetContentOf<T>(ContractPropertyCase contractPropertyCase = ContractPropertyCase.None) => DeserializationHelper.GetContentOf<T>(StringContent, contractPropertyCase);
        public bool TryGetContentOf<T>(out T content, ContractPropertyCase contractPropertyCase = ContractPropertyCase.None) => DeserializationHelper.TryGetContentOf(StringContent, out content, contractPropertyCase);
        public bool IsSuccessStatusCode => StatusCode >= HttpStatusCode.OK && StatusCode <= (HttpStatusCode) 299;
    }
    
    public class ApiResponse<T> : ApiResponse, IApiResponse<T>
    {
        public T Content { get; private set; }

        public static IApiResponse<T> FromResponseMessage(HttpResponseMessage responseMessage, T content, string stringContent)
        {
            var statusCode = responseMessage.StatusCode;
            var headers = new HeaderCollection(responseMessage.Headers);
            var contentHeaders = new HeaderCollection(responseMessage.Content.Headers);
            
            var apiResponse = new ApiResponse<T>
            {
                StatusCode = statusCode,
                Headers = headers,
                ContentHeaders = contentHeaders,
                StringContent = stringContent,
                Content = content
            };
            
            return apiResponse;
        }
    }
}