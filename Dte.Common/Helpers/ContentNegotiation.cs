using System.Collections.Generic;

namespace Dte.Common.Helpers
{
    public class ContentNegotiation
    {
        public readonly Dictionary<string, string> Headers = new Dictionary<string, string>();
        public string ContentType { get; set; }
        public string Accept { get; set; }
    }
}