using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Dte.Common.Contracts;

namespace Dte.Common.Helpers
{
    public class XmlSerializer : ISerializer
    {
        public string Serialize<TRequest>(TRequest request)
        {
            var settings = new XmlWriterSettings
            {
                Indent = false,
                NewLineHandling = NewLineHandling.None
            };
            
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            new System.Xml.Serialization.XmlSerializer(typeof (TRequest)).Serialize(xmlWriter, request, namespaces);
            var empty = stringWriter.ToString();
            xmlWriter.Close();
            stringWriter.Close();
            
            return empty;
        }

        public void TryDeserialize<TResponse>(string stringContent, out TResponse content)
        {
            using var stringReader = new StringReader(stringContent);
            var obj = new System.Xml.Serialization.XmlSerializer(typeof (TResponse)).Deserialize(stringReader);
            content = (TResponse) obj;
            stringReader.Close();
        }
    }
}