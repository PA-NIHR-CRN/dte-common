using System.IO;

namespace Dte.Common.Content
{
    public static class CustomMessageEmail
    {
        private const string BodyPlaceholder = "###BODY_REPLACE###";
        private const string InvisibleCognitoCodeParameter = "###INVISIBLE_COGNITO_CODE_PARAMETER###";
        private const string HtmlTemplatePath = "./EmailTemplate.html";
        
        public static string GetCustomMessageHtml(string bodyContent, string invisibleCognitoCodeParameter = "")
        {
            var htmlContent = File.ReadAllText(HtmlTemplatePath);

            htmlContent = htmlContent
                .Replace(BodyPlaceholder, bodyContent)
                .Replace(InvisibleCognitoCodeParameter, invisibleCognitoCodeParameter);

            return htmlContent;
        }
    }
}
