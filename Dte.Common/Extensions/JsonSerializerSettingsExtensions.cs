using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Dte.Common.Extensions
{
    public static class JsonSerializerSettingsExtensions
    {
        private static readonly Regex PascalCaseRegEx = new Regex("^[A-Z]", RegexOptions.Compiled);
        private static readonly Regex CamelCaseRegEx = new Regex("^[a-z]", RegexOptions.Compiled);

        public static void EnsurePropertiesArePascalCase(this JsonSerializerSettings settings) => settings.TraceWriter = new PropertyNameCasingTraceWriter("PascalCase", PascalCaseRegEx);
        public static void EnsurePropertiesAreCamelCase(this JsonSerializerSettings settings) => settings.TraceWriter = new PropertyNameCasingTraceWriter("CamelCase", CamelCaseRegEx);
    }
}