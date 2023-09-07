using System.Globalization;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Search;
using Dte.Common.Content;
using Dte.Common.Contracts;
using Dte.Common.Models;
using HandlebarsDotNet;

namespace Dte.Common.Services
{
    public class ContentfulService : IContentfulService
    {
        private readonly IContentfulClient _client;
        private readonly IRichTextToHtmlService _richTextToHtmlConverter;
        private const string BodyPlaceholder = "###BODY_REPLACE###";
        private const string DefaultLocale = "en-GB";

        public ContentfulService(IContentfulClient client, IRichTextToHtmlService richTextToHtmlConverter)
        {
            _client = client;
            _richTextToHtmlConverter = richTextToHtmlConverter;
        }

        public async Task<ContentfulEmail> GetContentfulEmailAsync(string entryId, CultureInfo locale)
        {
            return await _client.GetEntry(entryId,
                new QueryBuilder<ContentfulEmail>().LocaleIs(locale.ToString()));
        }

        public async Task<ContentfulEmailResponse> GetEmailContentAsync(EmailContentRequest request)
        {
            var selectedLocale = request.SelectedLocale ?? new CultureInfo(DefaultLocale);
            var contentfulEmail = await GetContentfulEmailAsync(request.EmailName, selectedLocale);

            return new ContentfulEmailResponse
            {
                EmailSubject = contentfulEmail.EmailSubject,
                EmailBody = ConstructEmailHtml(contentfulEmail.EmailBody, request, selectedLocale)
            };
        }

        private string ConstructEmailHtml(RichTextNode emailBody, EmailContentRequest request,
            CultureInfo selectedLocale)
        {
            string htmlContent = _richTextToHtmlConverter.Convert(emailBody, request.BaseUrl);
            var htmlTemplate = CustomMessageEmail.GetCustomMessageHtml().Replace(BodyPlaceholder, htmlContent);

            var data = new
            {
                link = request.Link,
                firstName = ToTitleCase(request.FirstName, selectedLocale)
            };

            var template = Handlebars.Compile(htmlTemplate);
            return template(data);
        }

        private static string ToTitleCase(string input, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace(input)
                ? string.Empty
                : cultureInfo.TextInfo.ToTitleCase(input.ToLower(cultureInfo));
        }
    }
}