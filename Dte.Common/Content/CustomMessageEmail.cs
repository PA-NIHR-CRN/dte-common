using System.IO;

namespace Dte.Common.Content
{
    public static class CustomMessageEmail
    {
        private const string BodyPlaceholder = "###BODY_REPLACE###";
        private const string HtmlTemplatePath = "./EmailTemplate.html";
        
        public static string GetCustomMessageHtml(string bodyContent)
        {
            var htmlContent = File.ReadAllText(HtmlTemplatePath);

            htmlContent = htmlContent
                .Replace(BodyPlaceholder, bodyContent);

            return htmlContent;
        }
    }
}
