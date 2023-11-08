using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dte.Common.Content
{
    public static class CustomMessageEmail
    {
        private const string BodyPlaceholder = "###BODY_REPLACE###";
        private const string EmailTemplateResourceSuffix = "EmailTemplate.html";
        private static readonly Lazy<string> CachedTemplate = new Lazy<string>(LoadTemplate);

        public static string GetCustomMessageHtml(string bodyContent)
        {
            // Use cached template if available
            string template = CachedTemplate.Value;

            if (string.IsNullOrEmpty(template))
            {
                throw new InvalidOperationException("Email template could not be loaded.");
            }

            return template.Replace(BodyPlaceholder, bodyContent);
        }

        private static string LoadTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(str => str.EndsWith(EmailTemplateResourceSuffix));

            if (resourceName == null)
            {
                throw new InvalidOperationException($"Resource with suffix {EmailTemplateResourceSuffix} not found.");
            }

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new InvalidOperationException($"Resource {resourceName} could not be loaded as a stream.");
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
