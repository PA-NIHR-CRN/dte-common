using System.Net.Http;

namespace Dte.Common.Http
{
    public static class ApiClientFactory
    {
        public static ApiClient For(HttpClient client, string serviceName)
        {
            return new ApiClient(client, ApiClientConfiguration.Default, serviceName);
        }
    }
}