using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Dte.Common.Helpers
{
    public static class HttpHeadersHelper
    {
        public static string[] GetHeadersArray(params HttpHeaders[] httpHeaders)
        {
            var array = httpHeaders
                .Where(x => x != null)
                .SelectMany(x => x.Select(header => header.Key + "=" + string.Join(";", header.Value)))
                .ToArray();

            
            return array;
        }
        
        public static string GetHeaderString(params HttpHeaders[] httpHeaders)
        {
            var sb = new StringBuilder();
            var array = GetHeadersArray(httpHeaders);

            sb.AppendLine("---------HEADERS----------");
            sb.AppendLine(string.Join(" | ", array));

            
            return sb.ToString();
        }
    }
}