using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Dte.Common.Http
{
    public interface IHeaderService
    {
        void ClearHeaders();
        void SetHeader(string key, string[] value);
        IEnumerable<string> GetHeader(string key);
        void AddAllHeadersToRequest(HttpRequestMessage httpRequestMessage);
        void AddHeaderToRequest(HttpRequestMessage httpRequestMessage, string key);
        void AddHeaderToRequest(HttpRequestMessage httpWebRequest, string key, string value);
        void AddHeaderToRequest(HttpRequestMessage httpWebRequest, string key, IEnumerable<string> values);
    }

    public class HeaderService : IHeaderService
    {
        private readonly ConcurrentDictionary<string, string[]> _headers = new ConcurrentDictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        public void ClearHeaders()
        {
            _headers.Clear();
        }

        public void SetHeader(string key, string[] values)
        {
            if (key == null) return;

            _headers[key] = values;
        }

        public IEnumerable<string> GetHeader(string key) => key != null && _headers.ContainsKey(key) ? _headers[key] : Array.Empty<string>();

        public void AddAllHeadersToRequest(HttpRequestMessage httpRequestMessage)
        {
            foreach (var key in _headers.Keys)
            {
                AddHeaderToRequest(httpRequestMessage, key);
            }
        }

        public void AddHeaderToRequest(HttpRequestMessage httpWebRequest, string key) => AddHeaderToRequest(httpWebRequest, key, GetHeader(key));

        public void AddHeaderToRequest(HttpRequestMessage httpWebRequest, string key, string value) => AddHeaderToRequest(httpWebRequest, key, new[] { value });

        public void AddHeaderToRequest(HttpRequestMessage httpWebRequest, string key, IEnumerable<string> values)
        {
            if (httpWebRequest.Headers.TryGetValues(key, out _))
            {
                httpWebRequest.Headers.Remove(key);
            }

            var list = values.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (list.Any())
            {
                httpWebRequest.Headers.Add(key, list);
            }
        }
    }
    
    public static class DefaultHeaders
    {
        public static string ConversationId = nameof (ConversationId);
        public const string UserAgent = "User-Agent";

        public static IEnumerable<string> All => new[] { ConversationId, UserAgent };
    }
}