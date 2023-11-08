using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dte.Common.Content
{
    public static class CustomMessageEmail
    {
        private const string BodyPlaceholder = "###BODY_REPLACE###";
        private const string EmailTemplateResourceSuffix = "Content.EmailTemplate.html";
        private static readonly Lazy<string> EmailTemplate = new Lazy<string>(LoadTemplateFromResource);

        public static string GetCustomMessageHtml(string bodyContent) => EmailTemplate.Value.Replace(BodyPlaceholder, bodyContent);

        private static string LoadTemplateFromResource()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                .Where(str => str.EndsWith(EmailTemplateResourceSuffix, StringComparison.InvariantCultureIgnoreCase))
                .DefaultIfEmpty(string.Empty)
                .SingleOrDefault();

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new FileNotFoundException($"Email template resource '{EmailTemplateResourceSuffix}' not found.");
            }

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
