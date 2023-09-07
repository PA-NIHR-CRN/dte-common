using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Search;
using Dte.Common.Content;
using Dte.Common.Contracts;
using Dte.Common.Models;
using HandlebarsDotNet;

namespace Dte.Common.Services
{
    public class ContentfulService: IContentfulService
    {
        private readonly IContentfulClient _client;
        private readonly IRichTextToHtmlService _richTextToHtmlConverter;

        public ContentfulService(IContentfulClient client, IRichTextToHtmlService richTextToHtmlConverter)
        {
            _client = client;
            _richTextToHtmlConverter = richTextToHtmlConverter;
        }

        public async Task<ContentfulEmail> GetContentfulEmailAsync(string entryId, string locale = "en-GB")
        {
            var entry = await _client.GetEntry(entryId, new QueryBuilder<ContentfulEmail>().LocaleIs(locale));
            return entry;
        }

        public async Task<ContentfulEmailResponse> GetEmailContentAsync(EmailContentRequest request)
        {
            var contentfulEmail = await GetContentfulEmailAsync(request.EmailName, request.SelectedLocale.ToString());
            string htmlContent = _richTextToHtmlConverter.Convert(contentfulEmail.EmailBody);

            var htmlBody = CustomMessageEmail.GetCustomMessageHtml().Replace("###BODY_REPLACE###", htmlContent);
            
            var data = new
            {
                request.Link,
                request.FirstName
            };

            var template = Handlebars.Compile(htmlBody);
            htmlBody = template(data);

            return new ContentfulEmailResponse
            {
                EmailSubject = contentfulEmail.EmailSubject,
                EmailBody = htmlBody
            };
        }
    }
}