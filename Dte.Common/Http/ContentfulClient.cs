using System.Net.Http;
using Contentful.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dte.Common.Http
{
    public static class ContentfulServiceExtensions
    {
        public static void AddContentfulServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddSingleton<IContentfulClient>(sp =>
            {
                var httpClient = sp.GetRequiredService<HttpClient>();
                var contentfulSettings = sp.GetRequiredService<ContentfulSettings>();
                return new ContentfulClient(httpClient, contentfulSettings.DeliveryApiKey,
                    contentfulSettings.PreviewApiKey, contentfulSettings.SpaceId, contentfulSettings.UsePreviewApi);
            });
        }
    }
}