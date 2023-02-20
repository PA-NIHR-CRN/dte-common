using System;
using System.Collections.Generic;
using System.Net.Http;
using Dte.Common.Contracts;

namespace Dte.Common.Helpers
{
    public class FormSerializer : ISerializer
    {
        public string Serialize<TRequest>(TRequest request) => (object) request is IDictionary<string, string> ? new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) request).ReadAsStringAsync().Result : throw new InvalidOperationException("Form data must be an IDictionary<string,string>");
        public void TryDeserialize<TResponse>(string stringContent, out TResponse content) => throw new InvalidOperationException("Cannot deserialize form data yet");
    }
}