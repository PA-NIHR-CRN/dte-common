using System.Net;

namespace Dte.Common.Responses
{
    public interface IApiResponse
    {
        HttpStatusCode StatusCode { get; }
        HeaderCollection Headers { get; }
        HeaderCollection ContentHeaders { get; }
        string StringContent { get; }
        T GetContentOf<T>(ContractPropertyCase contractPropertyCase = ContractPropertyCase.None);
        bool TryGetContentOf<TResponse>(out TResponse content, ContractPropertyCase contractPropertyCase = ContractPropertyCase.None);
        bool IsSuccessStatusCode { get; }
    }
    
    public interface IApiResponse<out T> : IApiResponse
    {
        T Content { get; }
    }
}