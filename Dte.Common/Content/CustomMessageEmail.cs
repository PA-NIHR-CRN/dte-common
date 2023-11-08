using System.IO;
using System.Linq;

namespace Dte.Common.Content
{
    public static class CustomMessageEmail
    {
        private const string BodyPlaceholder = "###BODY_REPLACE###";
        
        public static string GetCustomMessageHtml(string bodyContent)
        {
            // get the html template as an embedded resource, so that it is included in the assembly
            var assembly = typeof(CustomMessageEmail).Assembly;
            var resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("EmailTemplate.html")); 
            var stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            var htmlContent = reader.ReadToEnd();

            htmlContent = htmlContent
                .Replace(BodyPlaceholder, bodyContent);

            return htmlContent;
        }
    }
}
