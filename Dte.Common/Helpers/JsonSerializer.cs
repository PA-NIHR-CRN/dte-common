using Dte.Common.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Dte.Common.Helpers
{
    public class JsonSerializer : ISerializer
    {
        private readonly ContractPropertyCase _contractPropertyCase;
        private readonly JsonSerializerSettings _settings;

        public JsonSerializer(ContractPropertyCase contractPropertyCase)
        {
            _contractPropertyCase = contractPropertyCase;
            
            _settings = new JsonSerializerSettings
            {
                ContractResolver = _contractPropertyCase == ContractPropertyCase.CamelCase ? new CamelCasePropertyNamesContractResolver() : (IContractResolver) new DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _settings.Converters.Add(new StringEnumConverter());
        }

        public string Serialize<TRequest>(TRequest request) => JsonConvert.SerializeObject((object) request, Formatting.None, this._settings);
        public void TryDeserialize<TResponse>(string stringContent, out TResponse content) => DeserializationHelper.TryGetContentOf<TResponse>(stringContent, out content, this._contractPropertyCase);
    }
}