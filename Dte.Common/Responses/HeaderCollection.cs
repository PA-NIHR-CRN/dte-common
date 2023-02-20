using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Dte.Common.Responses
{
    public class HeaderCollection : List<Header>
    {
        public HeaderCollection(IEnumerable<Header> collection) : base(collection)
        {
        }

        public HeaderCollection(HttpHeaders collection) : base(collection.Select(_ => new Header(_.Key, _.Value.FirstOrDefault())))
        {
        }

        public string this[string name] => this.FirstOrDefault(h => h.Name == name)?.Value;
    }
    
    public class Header
    {
        public string Name { get; }
        public string Value { get; }

        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString() => Name + "=" + Value;
    }
}