using Dte.Common.Extensions;
using Newtonsoft.Json;

namespace Dte.Common.Helpers
{
    public static class DeserializationHelper
    {
        public static T GetContentOf<T>(string value, ContractPropertyCase contractPropertyCase = ContractPropertyCase.None)
        {
            var serializerSettings = CreateSerializerSettings(contractPropertyCase);
            
            return JsonConvert.DeserializeObject<T>(value, serializerSettings);
        }

        public static bool TryGetContentOf<T>(string value, out T content, ContractPropertyCase contractPropertyCase = ContractPropertyCase.None)
        {
            var serializerSettings = CreateSerializerSettings(contractPropertyCase);
            try
            {
                content = JsonConvert.DeserializeObject<T>(value, serializerSettings);
                
                return true;
            }
            catch
            {
                content = default;
                
                return false;
            }
        }

        private static JsonSerializerSettings CreateSerializerSettings(ContractPropertyCase contractPropertyCase)
        {
            var settings = new JsonSerializerSettings();
            
            switch (contractPropertyCase)
            {
                case ContractPropertyCase.CamelCase:
                    settings.EnsurePropertiesAreCamelCase();
                    break;
                case ContractPropertyCase.PascalCase:
                    settings.EnsurePropertiesArePascalCase();
                    break;
            }

            return settings;
        }
    }
}