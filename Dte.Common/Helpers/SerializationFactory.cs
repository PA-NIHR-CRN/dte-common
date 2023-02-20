using Dte.Common.Contracts;

namespace Dte.Common.Helpers
{
    public static class SerializationFactory
    {
        public static ISerializer GetSerializer(string contentType, ContractPropertyCase contractPropertyCase)
        {
            if (contentType == "application/xml" || contentType == "text/xml")
            {
                return new XmlSerializer();
            }
            
            return contentType == "application/x-www-form-urlencoded" ? new FormSerializer() : (ISerializer) new JsonSerializer(contractPropertyCase);
        }
    }
}