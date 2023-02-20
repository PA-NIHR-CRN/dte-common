using System;
using System.Text;
using Dte.Common.Contracts;

namespace Dte.Common.Extensions
{
    public static class ApiClientExtensions
    {
        public static void SetBasicAuthorisationHeader(this IApiClient apiClient, string username, string password)
        {
            var authString = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            apiClient.SetDefaultRequestHeaders("Authorization", authString);
        }
        
        public static void SetBearerAuthorisationHeader(this IApiClient apiClient, string token) => apiClient.SetDefaultRequestHeaders("Authorization", "Bearer " + token);

        public static void SetConversationIdHeader(this IApiClient apiClient, string conversationId = null) => apiClient.SetDefaultRequestHeaders("ConversationId", conversationId ?? Guid.NewGuid().ToString());
    }
}