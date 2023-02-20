using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Dte.Common.Exceptions.Common
{
    [Serializable]
    public class Error : IExtensibleDataObject
    {
        [JsonIgnore]
        [NonSerialized]
        private ExtensionDataObject _extensionData;

        public Error() { }
        
        [JsonConstructor]
        private Error(string customCode, string detail)
        {
            CustomCode = customCode;
            Detail = detail;
        }

        public Error(string service, string component, string customCode, string detail) 
            : this(customCode, detail)
        {
            Service = service;
            Component = component;
        }
        
        public Error(string service, string component, string exceptionName, string customCode, string detail) 
            : this(service, component, customCode, detail)
        {
            ExceptionName = exceptionName;
            HttpStatusName = System.Net.HttpStatusCode.InternalServerError.ToString();
            HttpStatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        }
        
        public Error(string service, string component, string exceptionName, string httpStatusName, int httpStatusCode, string httpResponseString, string customCode, string detail) 
            : this(service, component, exceptionName, customCode, detail)
        {
            HttpStatusName = httpStatusName;
            HttpStatusCode = httpStatusCode;
            HttpResponseString = httpResponseString;
        }

        [DataMember(EmitDefaultValue = false, Name = "service")]
        [XmlElement(ElementName = "service")]
        public string Service { get; set; }
        
        [DataMember(EmitDefaultValue = false, Name = "component")]
        [XmlElement(ElementName = "component")]
        public string Component { get; set; }
        
        [DataMember(EmitDefaultValue = false, Name = "exceptionName")]
        [XmlElement(ElementName = "exceptionName")]
        public string ExceptionName { get; set; }
        
        [DataMember(EmitDefaultValue = false, Name = "httpStatusName")]
        [XmlElement(ElementName = "httpStatusName")]
        public string HttpStatusName { get; set; }
        
        [DataMember(EmitDefaultValue = false, Name = "httpStatusCode")]
        [XmlElement(ElementName = "httpStatusCode")]
        public int HttpStatusCode { get; set; }
        
        [DataMember(EmitDefaultValue = false, Name = "httpResponseString")]
        [XmlElement(ElementName = "httpResponseString")]
        public string HttpResponseString { get; set; }
        
        [DataMember(EmitDefaultValue = false, Name = "customCode")]
        [XmlElement(ElementName = "customCode")]
        public string CustomCode { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "detail")]
        [XmlElement(ElementName = "detail")]
        public string Detail { get; set; }

        [JsonIgnore]
        public ExtensionDataObject ExtensionData
        {
            get => _extensionData;
            set => _extensionData = value;
        }
    }
}